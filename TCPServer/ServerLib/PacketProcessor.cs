using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks.Dataflow;

using CommonServerLib;
using CSBaseLib;

namespace ServerLib
{
    // 패킷 처리 클래스. 
    class PacketProcessor
    {
        bool IsThreadRunning = false;

        // 패킷을 처리할 스레드
        System.Threading.Thread ProcessThread = null;
        
        // 처리할 패킷을 저장할 저장소.
        BufferBlock<ServerPacketData> MsgBuffer = new BufferBlock<ServerPacketData>();

        // 패킷 핸들러 저장소
        Dictionary<int, Action<ServerPacketData, ConnectUser>> PacketHandlerMap = new Dictionary<int, Action<ServerPacketData, ConnectUser>>();
        
        // 공용 패킷 처리 핸들러 클래스
        PKHCommon CommonPacketHandler = null;
        // 로그인 관련 패킷 처리 핸들러 클래스
        PKHLogInOut LogInOutPacketHandler = null;
        // 로비 관련 패킷 처리 핸들러 클래스
        PKHLobby LobbyPacketHandler = null;

        UserManager ConnectedUserManager = null;

        LobbyManager LobbyMgr = null;


        public void CreateAndStart(ServerNetwork mainServer, PacketDistributor packetDistributor)
        {
            var lobbyCount = ServerEnvironment.LobbyCount;
            var lobbyBeginIndex = ServerEnvironment.LobbyStartIndex;
            var lobbyEndIndex = lobbyBeginIndex + lobbyCount;
            var maxLobbyUserCount = ServerEnvironment.MaxUserPerLobby;
            var totalUserCount = lobbyCount * maxLobbyUserCount;
            

            ConnectedUserManager = new UserManager();
            ConnectedUserManager.Init(mainServer, (mainServer.Config.MaxConnectionNumber+100));


            LobbyMgr = new LobbyManager();
            LobbyMgr.CreateLobby(mainServer, lobbyCount, lobbyBeginIndex, maxLobbyUserCount);


            RegistPacketHandler(mainServer, packetDistributor, ConnectedUserManager);

            IsThreadRunning = true;
            ProcessThread = new System.Threading.Thread(this.Process);
            ProcessThread.Start();
        }
        
        public void Destory()
        {
            IsThreadRunning = false;
            MsgBuffer.Complete();
        }

        public LobbyManager GetLobbyManager() { return LobbyMgr; }

        public void InsertMsg(bool isClientRequest, ServerPacketData data)
        {
            try
            {
                if (isClientRequest &&
                    data.PacketID.InRange((int)PACKETID.CS_BEGIN, (int)PACKETID.CS_END) == false
                    )
                {
                    SendWrongUserPacketToSystem(WRONG_USER_TYPE.INVALID_PACKET_ID_RANGE, data.SessionID);
                    return;
                }

                MsgBuffer.Post(data);
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        void RegistPacketHandler(ServerNetwork mainServer, PacketDistributor packetDistributor, UserManager userManager)
        {
            CommonPacketHandler = new PKHCommon();
            CommonPacketHandler.Init(mainServer, packetDistributor, userManager, LobbyMgr);
            CommonPacketHandler.CreateBroadcastMessagManager(LobbyMgr, 한번에_보낼_로비_수: 5);

            LogInOutPacketHandler = new PKHLogInOut();
            LogInOutPacketHandler.Init(mainServer, packetDistributor, userManager, LobbyMgr);
            
            LobbyPacketHandler = new PKHLobby();
            LobbyPacketHandler.Init(mainServer, packetDistributor, userManager, LobbyMgr);


            PacketHandlerMap.Add((int)PACKETID.SYSTEM_CONNECT_CLIENT, CommonPacketHandler.SystemConnectClient);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_DISCONNECT_CLIENT, CommonPacketHandler.SystemDisConnectClient);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_LOBBY_INFO_UPDATE, CommonPacketHandler.SystemLobbyInfoUpdate);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_READ_SERVER_MESSAGE, CommonPacketHandler.SystemReadServerMessage);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_READ_S2S_MESSAGE, CommonPacketHandler.ResponseDBReadS2SMessage);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_CHECK_USER_STATUS, CommonPacketHandler.SystemCheckUserStatus);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_WRONG_USER, CommonPacketHandler.SystemWrongUser);
            PacketHandlerMap.Add((int)PACKETID.REQ_HEART_BEAT, CommonPacketHandler.RequestHeartBeat);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_BROADCAST_MESSAG, CommonPacketHandler.SystemBroadcastMessag);
            PacketHandlerMap.Add((int)PACKETID.SYSTEM_UI_UPDATE, CommonPacketHandler.SystemUIUpdate);
            
            PacketHandlerMap.Add((int)PACKETID.REQ_LOGIN, LogInOutPacketHandler.RequestLogin);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_LOGIN, LogInOutPacketHandler.ResponseLoginFromDB);
            PacketHandlerMap.Add((int)PACKETID.REQ_OWN_DISCONNECT, LogInOutPacketHandler.RequestOwnDisconnect);
            
            PacketHandlerMap.Add((int)PACKETID.REQ_ENTER_LOBBY, LobbyPacketHandler.RequestEnterLobby);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_ENTER_LOBBY, LobbyPacketHandler.ResponseDBEnterLobby);
            PacketHandlerMap.Add((int)PACKETID.REQ_LEAVE_LOBBY, LobbyPacketHandler.RequestLeaveLobby);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_LEAVE_LOBBY, LobbyPacketHandler.ResponseDBLeaveLobby);
            PacketHandlerMap.Add((int)PACKETID.REQ_LOBBY_CHAT, LobbyPacketHandler.RequestLobbyChat);
            PacketHandlerMap.Add((int)PACKETID.REQ_GUILD_CHAT, LobbyPacketHandler.RequestGuildChat);
            PacketHandlerMap.Add((int)PACKETID.REQ_LOAD_USER_GUILD_INFO, LobbyPacketHandler.RequestLoadUserGuildInfo);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_LOAD_USER_GUILD_INFO, LobbyPacketHandler.ResponseLoadUserGuildInfo);
        }

        // 패킷을 처리한다.
        void Process()
        {
            while (IsThreadRunning)
            {
                try
                {
                    var packet = MsgBuffer.Receive();

                    if (PacketHandlerMap.ContainsKey(packet.PacketID))
                    {
                        var user = ConnectedUserManager.GetUserSessionID(packet.SessionID);

                        if (user != null) 
                        {
                            if (user.IsBlock)
                            {
                                (DevLog.IsEnable(LOG_STRING_TYPE.BLOCK_USER)).IfTrue(() => DevLog.Write(string.Format("IsBlock User. Session:{0}, ID{1}", packet.SessionID, user.UserID), LOG_LEVEL.TRACE));
                                continue;
                            }

                            if (packet.PacketID.InRange((int)PACKETID.CS_BEGIN, (int)PACKETID.CS_END))
                            {
                                user.RecordPacketReceiveTime(Util.TimeTickToSec(DateTime.Now.Ticks));
                            }
                        }

                        PacketHandlerMap[packet.PacketID](packet, user);
                    }
                    else
                    {
                        SendWrongUserPacketToSystem(WRONG_USER_TYPE.INVALID_PACKET_ID, packet.SessionID);
                        System.Diagnostics.Debug.WriteLine("세션 번호 {0}, PacketID {1}, 받은 데이터 크기: {2}", 
                                        packet.SessionID, packet.PacketID, packet.JsonFormatData.Length);
                    }
                }
                catch(Exception ex)
                {
                    IsThreadRunning.IfTrue(() => DevLog.Write(ex.ToString(), LOG_LEVEL.ERROR));
                }
            }
        }

        // 어떤 종료의 유저 상태를 조사할지 설정한다.
        public void SetUserStatusCheckOption(UserStatusCheckOption option)
        {
            ConnectedUserManager.SetUserStatusCheckOption(option);
        }

        // 잘못된 유저임을 알리는 메시지를 내부에 보낸다.
        public void SendWrongUserPacketToSystem(WRONG_USER_TYPE type, string sessionID)
        {
            try
            {
                var systemPacket = ServerPacketData.MakeNTFWrongUserPacket(WRONG_USER_TYPE.INVALID_PACKET_ID, sessionID);
                MsgBuffer.Post(systemPacket);
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }


        // 개발용
        public void Dev공지보내기(string message)
        {
            CommonPacketHandler.AddAllUserNotifyMessage(message);
        }


    }
}
