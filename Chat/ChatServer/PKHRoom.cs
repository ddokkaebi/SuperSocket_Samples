using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessagePack;

using CSBaseLib;
using CommonServerLib;


namespace ChatServer
{
    public class PKHRoom : PKHandler
    {
        List<Room> RoomList = null;
        
        public void Init(List<Room> roomList)
        {
            RoomList = roomList;
        }

        public void RequestEnter(ServerPacketData packetData)
        {

        }

        public void RequestLeave(ServerPacketData packetData)
        {

        }

        public void RequestChar(ServerPacketData packetData)
        {

        }
        //public void RequestLogin(ServerPacketData packetData)
        //{
        //    var sessionID = packetData.SessionID;
        //    var sessionIndex = packetData.SessionIndex;
        //    DevLog.Write("로그인 요청 받음", LOG_LEVEL.DEBUG);

        //    try
        //    {
        //        if( SessionManager.EnableReuqestLogin(sessionIndex) == false)
        //        {
        //            ResponseLoginToClient(ERROR_CODE.LOGIN_ALREADY_WORKING, packetData.SessionID);
        //            return;
        //        }

        //        var reqData = MessagePackSerializer.Deserialize< PKTReqLogin>(packetData.BodyData);

        //        // 세션의 상태를 바꾼다
        //        SessionManager.SetPreLogin(sessionIndex);

        //        // DB 작업 의뢰한다.
        //        var dbReqLogin = new DBReqLogin() { AuthToken = reqData.AuthToken };
        //        var jobDatas = MessagePackSerializer.Serialize(dbReqLogin);

        //        var dbQueue = MakeDBQueue(PACKETID.REQ_DB_LOGIN, sessionID, sessionIndex, jobDatas);
        //        RequestDBJob(ServerNetwork.GetPacketDistributor(), dbQueue);

        //        DevLog.Write("DB에 로그인 요청 보냄", LOG_LEVEL.DEBUG);
        //    }
        //    catch(Exception ex)
        //    {
        //        // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
        //        DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
        //    }
        //}


        //public void ResponseLoginToClient(ERROR_CODE errorCode, string sessionID)
        //{
        //    var resLogin = new PKTResLogin()
        //    {
        //        Result = (short)errorCode
        //    };

        //    var Body = MessagePackSerializer.Serialize(resLogin);
        //    var sendData = PacketToBytes.Make(PACKETID.RES_LOGIN, 0, Body);

        //    ServerNetwork.SendData(sessionID, sendData);
        //}

    }
}
