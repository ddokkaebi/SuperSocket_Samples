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
    public class PKHCommon : PKHandler
    {
        //TODO: 제거해야 할 듯
        //UserManager ClientUserManager = new UserManager();

                
        public void NotifyInConnectClient(ServerPacketData requestData)
        {
            requestData = null;

            InnerMessageHostProgram.CurrentUserCount(ServerNetwork.SessionCount);

            //TODO 최대 유저 수가 넘는 경우 여기서 접속 종료를 통보한다
            // 로그인 요청도 못하게 관련 flag을 셋해야 한다.
        }

        public void NotifyInDisConnectClient(ServerPacketData requestData)
        {
            requestData = null;

            InnerMessageHostProgram.CurrentUserCount(ServerNetwork.SessionCount);
        }


        public void RequestLogin(ServerPacketData packetData)
        {
            var sessionID = packetData.SessionID;
            var sessionIndex = packetData.SessionIndex;
            DevLog.Write("로그인 요청 받음", LOG_LEVEL.DEBUG);

            try
            {
                if( SessionManager.EnableReuqestLogin(sessionIndex) == false)
                {
                    ResponseLoginToClient(ERROR_CODE.LOGIN_ALREADY_WORKING, packetData.SessionID);
                    return;
                }

                var reqData = MessagePackSerializer.Deserialize< PKTReqLogin>(packetData.BodyData);

                // 세션의 상태를 바꾼다
                SessionManager.SetPreLogin(sessionIndex);
                
                // DB 작업 의뢰한다.
                var dbReqLogin = new DBReqLogin() { AuthToken = reqData.AuthToken };
                var jobDatas = MessagePackSerializer.Serialize(dbReqLogin);
                
                var dbQueue = MakeDBQueue(PACKETID.REQ_DB_LOGIN, sessionID, sessionIndex, jobDatas);
                RequestDBJob(ServerNetwork.GetPacketDistributor(), dbQueue);
                                
                DevLog.Write("DB에 로그인 요청 보냄", LOG_LEVEL.DEBUG);
            }
            catch(Exception ex)
            {
                // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
                DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            }
        }

        public void ResponseLoginFromDB(ServerPacketData packetData)
        {
            var sessionIndex = packetData.SessionIndex;
            DevLog.Write("DB에서 로그인 답변 받음", LOG_LEVEL.DEBUG);

            try
            {
                var resData = MessagePackSerializer.Deserialize<DBResLogin>(packetData.BodyData);
                
                // DB 처리 성공/실패에 대한 처리를 한다.
                var errorCode = ERROR_CODE.NONE;

                if (resData.Result == ERROR_CODE.NONE)
                {
                    SessionManager.SetLogin(sessionIndex, resData.UserID);
                }
                else
                {
                    errorCode = ERROR_CODE.LOGIN_INVALID_AUTHTOKEN;
                    SessionManager.SetStateNone(sessionIndex);
                }
                
                ResponseLoginToClient(errorCode, packetData.SessionID);
                
                DevLog.Write("로그인 요청 답변 보냄", LOG_LEVEL.DEBUG);
            }
            catch (Exception ex)
            {
                // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
                DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            }
        }

        public void ResponseLoginToClient(ERROR_CODE errorCode, string sessionID)
        {
            var resLogin = new PKTResLogin()
            {
                Result = (short)errorCode
            };

            var Body = MessagePackSerializer.Serialize(resLogin);
            var sendData = PacketToBytes.Make(PACKETID.RES_LOGIN, 0, Body);

            ServerNetwork.SendData(sessionID, sendData);
        }


        
        
        // 테스트 ------------------------------------------------------------
        public void RequestTestEcho(ServerPacketData requestData)
        {
            var session = ServerNetwork.GetSessionByID(requestData.SessionID);

            if(session == null)
            {
                return;
            }

            List<byte> dataSource = new List<byte>();
            dataSource.AddRange(BitConverter.GetBytes((Int32)PACKETID.RES_TEST_ECHO));
            dataSource.AddRange(BitConverter.GetBytes((Int16)0));
            dataSource.AddRange(BitConverter.GetBytes((Int16)0));
            dataSource.AddRange(BitConverter.GetBytes((Int32)requestData.BodyData.Length));
            dataSource.AddRange(requestData.BodyData);

            session.Send(dataSource.ToArray(), 0, dataSource.Count);
        }
    }
}
