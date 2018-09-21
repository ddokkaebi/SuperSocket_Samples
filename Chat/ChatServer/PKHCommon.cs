using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MsgPack.Serialization;

using CSBaseLib;
using CommonServerLib;


namespace ChatServer
{
    public class PKHCommon : PKHandler
    {
        MainServer ServerNetwork;
        UserManager ClientUserManager = new UserManager();


        public void Init(MainServer serverNetwork)
        {
            ServerNetwork = serverNetwork;
        }

        public void NotifyInConnectClient(ServerPacketData requestData)
        {
            requestData = null;

            InnerMessageHostProgram.CurrentUserCount(ServerNetwork.SessionCount);
        }

        public void NotifyInDisConnectClient(ServerPacketData requestData)
        {
            requestData = null;

            InnerMessageHostProgram.CurrentUserCount(ServerNetwork.SessionCount);
        }


        public void RequestLogin(ServerPacketData packetData)
        {
            DevLog.Write("로그인 요청 받음", LOG_LEVEL.DEBUG);

            try
            {
                var deSerializer = MessagePackSerializer.Create<PKTReqLogin>();
                var reqData = deSerializer.UnpackSingleObject(packetData.BodyData);

                // 일단 임시로 유저 등록을 한다.
                var error = ClientUserManager.AddUser(reqData.UserID, packetData.SessionID);

                if(error != ERROR_CODE.NONE)
                {
                    ResponseLoginToClient(error, packetData.SessionID);
                    return;
                }


                // DB 작업 의뢰한다.
                var dbReqLogin = new DBReqLogin() { AuthToken = reqData.AuthToken };
                var serializer = MessagePackSerializer.Create<DBReqLogin>();
                var jobDatas = serializer.PackSingleObject(dbReqLogin);
                
                var dbQueue = MakeDBQueue(PACKETID.REQ_DB_LOGIN, packetData.SessionID, packetData.Value1, reqData.UserID, jobDatas);
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
            DevLog.Write("DB에서 로그인 답변 받음", LOG_LEVEL.DEBUG);

            try
            {
                var deSerializer = MessagePackSerializer.Create<DBResLogin>();
                var resData = deSerializer.UnpackSingleObject(packetData.BodyData);


                // DB 처리 성공/실패에 대한 처리를 한다.
                var errorCode = ERROR_CODE.NONE;

                if (resData.Result == ERROR_CODE.NONE)
                {
                    errorCode = ClientUserManager.유저_인증_완료(resData.UserID);
                }
                else
                {
                    errorCode = ERROR_CODE.LOGIN_INVALID_AUTHTOKEN;
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

            var serializer = MessagePackSerializer.Create<PKTResLogin>();
            var Body = serializer.PackSingleObject(resLogin);
            var sendData = PacketToBytes.Make(PACKETID.RES_LOGIN, 0, Body);

            ServerNetwork.SendData(sessionID, sendData);
        }


        public void RequestLogout(ServerPacketData requestData)
        {
            DevLog.Write("로그아웃 요청 받음", LOG_LEVEL.DEBUG);

            try
            {
                var resLogout = new PKTResLogout()
                {
                    Result = (short)ERROR_CODE.NONE
                };

                var serializer = MessagePackSerializer.Create<PKTResLogout>();
                var Body = serializer.PackSingleObject(resLogout);
                var sendData = PacketToBytes.Make(PACKETID.REQ_LOGOUT, 0, Body);

                ServerNetwork.SendData(requestData.SessionID, sendData);

                DevLog.Write("로그아웃 요청 답변 보냄", LOG_LEVEL.DEBUG);
            }
            catch (Exception ex)
            {
                // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
                DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            }
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
