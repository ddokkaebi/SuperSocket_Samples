using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;


namespace ServerLib
{
    // (네트워크)접속한 유저
    public class ConnectUser
    {
        // 고유 번호. 처음 할당 후 바뀌지 않는다.
        public int UserIndex { get; private set; }

        // 일련 번호. 이 객체를 재사용할 때 마다 새로 할당 받는다.
        public Int64 SequenceNumber { get; private set; }

        public string SessionID { get; private set; }
        
        public string UserID { get; private set; }
        
        public string NickName { get; private set; }

        public Int64 GuildUnique { get; private set; }
        
        // 인증 완료 여부. true이면 인증 완료
        public bool Authorized { get; private set; }
        
        public int LobbyID { get; private set; }

        // 로비에서의 상태
        public LOBBYUSER_STATUS Status;

        // 네트워크 통신 가능 여부
        bool EnableNetwork = false;

        // true 이면 요청을 받지 않는다.
        public bool IsBlock { get; private set; }

        public Int64 최근_패킷_받은_시간_초 { get; private set; }
        public Int64 접속_시간_초 { get; private set; }
        public Int64 로비_대기_시작_시간_초 { get; private set; }
        public Int64 접속_종료_예약_시간_초 { get; private set; }

        // 유저가 보낸 패킷 정보 관리
        UserRequestPacketCountInfo RequestCountInfo = new UserRequestPacketCountInfo();

        // 자신이 자신을 잘라 달라고 했을 때의 정보
        OwnReqDisconnectInfo OwnReqDisconInfo = new OwnReqDisconnectInfo();


        public void Init(int index)
        {
            UserIndex = index;

            RequestCountInfo.Init(ServerDefineData.MAX_CLIENT_REQUEST_RECORD_COUNT, 
                                ServerDefineData.MAX_CLIENT_REQUEST_PER_SECOND);
            Clear();
        }

        public void Clear()
        {
            SequenceNumber = 0;
            SessionID = "";
            UserID = "";
            NickName = "";
            GuildUnique = 0;
            Authorized = false;
            LobbyID = 0;

            SetPacketReceiveTimeSec(DateTime.Now.Ticks);
            접속_시간_초 = 0;
            LeaveLobby(0);

            EnableNetwork = false;
            Status = LOBBYUSER_STATUS.NONE;
            IsBlock = false;
            접속_종료_예약_시간_초 = 0;

            RequestCountInfo.Clear();

            최근_패킷_받은_시간_초 = CommonServerLib.Util.TimeTickToSec(DateTime.Now.Ticks);

            OwnReqDisconInfo.Clear();
        }
            
        // 이 객체를 사용한다.
        public void SetUse(Int64 sequenceNumber, string sessionID, Int64 curTimeSec)
        {
            Authorized = false;
            SequenceNumber = sequenceNumber;
            SessionID = sessionID;
            접속_시간_초 = curTimeSec;
            로비_대기_시작_시간_초 = curTimeSec;
            EnableNetwork = true;

            최근_패킷_받은_시간_초 = CommonServerLib.Util.TimeTickToSec(DateTime.Now.Ticks);
        }

        void SetID(string userID) { UserID = userID; }

        // 인증 되었음으로 설정
        public bool SetAuthorized(string userID, string nickName)
        {
            if (Authorized)
            {
                return false;
            }

            Authorized = true;
            SetID(userID);
            NickName = nickName;
            
            return true;
        }

        // 이 객체 사용 여부. true이면 사용 하지 않는 중
        public bool IsUnUse() { return (SequenceNumber == 0 && 접속_시간_초 == 0) ? true : false; }

        public void EnterLobby(int lobbyID, Int64 curTimeSec)
        {
            LobbyID = lobbyID;
            로비_대기_시작_시간_초 = curTimeSec;
            Status = LOBBYUSER_STATUS.PREPARE;
        }

        // 로비 입장이 완료로 상태 변경
        public void ChangeStatusToEnterLobbyComplete()
        {
            Status = LOBBYUSER_STATUS.COMPLETE;
        }
        
        public void LeaveLobby(Int64 curTimeSec)
        {
            LobbyID = 0;
            로비_대기_시작_시간_초 = curTimeSec;
        }

        public bool EnteredLobby() 
        { 
            return (LobbyID != 0 && Status == LOBBYUSER_STATUS.COMPLETE) ? true : false; 
        }
        
        // 패킷을 받은 시간 저장
        public void SetPacketReceiveTimeSec(Int64 curTimeTick)
        {
            EnableNetwork = true;
            최근_패킷_받은_시간_초 = CommonServerLib.Util.TimeTickToSec(curTimeTick);
        }

        // 네트워크 사용 불가로 설정
        public void AbnormalNetwork() { EnableNetwork = false; }

        //로비에서 네트워크 통신 가능 여부
        public bool EnableNetworkInLobby()
        {
            return (Status == LOBBYUSER_STATUS.COMPLETE && EnableNetwork) ? true : false;
        }

        // 불법 유저 설정
        public void SetWrongUser(Int64 timeSec)
        {
            AbnormalNetwork();
            IsBlock = true;
            접속_종료_예약_시간_초 = timeSec;
        }

        public void RecordPacketReceiveTime(Int64 curTimeSec)
        {
            RequestCountInfo.Record(curTimeSec);
        }

        // 너무 자주 요청 하는 유저인지 조사한다. true이면 비정상적으로 요청이 많은 유저 
        public bool CheckHeavuRequest()
        {
            return RequestCountInfo.CheckHeavuRequest();
        }

        public bool ValidGuild()
        {
            return (GuildUnique > 0) ? true : false;
        }

        public void SetGuildUnique(Int64 guildUnique)
        {
            GuildUnique = guildUnique;
        }


        public void OwnDisconnectUserInfo(int lobbyID)
        {
            OwnReqDisconInfo.IsRequested = true;
            OwnReqDisconInfo.LobbyID = lobbyID;
        }

        public int OwnDisconnectUserLobbyId()
        {
            if (OwnReqDisconInfo.IsRequested == false)
            {
                return 0;
            }

            return OwnReqDisconInfo.LobbyID;
        }
    }


    public enum LOBBYUSER_STATUS
    {
        NONE    = 0,
        PREPARE = 1,    // 로비에 들어 왔으나 아직 확인 절차가 남음
        COMPLETE = 2,    // 로비에 들어가기 완료
    }

    // 가기 자신이 잘라 달라고 했을 때의 정보
    class OwnReqDisconnectInfo
    {
        public bool IsRequested;
        public int LobbyID;

        public void Clear()
        {
            IsRequested = false;
            LobbyID = 0;
        }
    }
}
