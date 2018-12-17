using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using CommonServerLib;


namespace ServerLib
{
    class PKHLogInOut : PKBaseHandler
    {
        public void RequestLogin(ServerPacketData packetData, ConnectUser user)
        {
            //DevLog.Write("로그인 요청 받음", LOG_LEVEL.TRACE);
            //try
            //{
            //    if (user == null || user.Authorized)
            //    {
            //        return;
            //    }

            //    if (ChatServerEnvironment.MaxUserCount <= UserManagerRef.TotalConnectUserCount())
            //    {
            //        ResponseLoginToClient(ERROR_CODE.ADD_USER_FULL, packetData.SessionID);
            //        return;
            //    }

            //    var reqData = JsonConvert.DeserializeObject<PKTReqLogin>(packetData.JsonFormatData);

            //    var dbReqLogin = new DBReqLogin() { UserID = reqData.UserID, AuthToken = reqData.AuthToken };
            //    RequestDBJob<DBReqLogin>(dbReqLogin, PACKETID.REQ_DB_LOGIN, packetData.SessionID);

            //    DevLog.Write(string.Format("DB에 로그인 요청 보냄: SessionID:{0}, UserID:{1}", user.SessionID, reqData.UserID), LOG_LEVEL.TRACE);
            //    //ServerLogic.WriteFileLog(string.Format("DB에 로그인 요청 보냄: SessionID:{0}, UserID:{1}", user.SessionID, reqData.UserID), LOG_LEVEL.DEBUG);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        public void ResponseLoginFromDB(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("DB에서 로그인 답변 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var resData = JsonConvert.DeserializeObject<DBResLogin>(packetData.JsonFormatData);

            //    // DB 처리 성공/실패에 대한 처리를 한다.
            //    var errorCode = ERROR_CODE.NONE;

            //    if (resData.Result == ERROR_CODE.NONE)
            //    {
            //        errorCode = UserManagerRef.유저_인증_완료(packetData.SessionID, 
            //                                        resData.UserID, resData.NickName, resData.GuildUnique);
            //    }
            //    else
            //    {
            //        errorCode = ERROR_CODE.LOGIN_INVALID_AUTHTOKEN;
            //    }

            //    ResponseLoginToClient(errorCode, packetData.SessionID);


            //    // 기존 유저를 짜른다. 지금 인증 요청한 유저는 위의 답변을 받은 후 3초 후에 접속을 다시 시도한다.
            //    if (errorCode == ERROR_CODE.ADD_USER_DUPLICATION_USERID)
            //    {
            //        var sessionID = UserManagerRef.GetUserID(resData.UserID).SessionID;

            //        DevLog.Write(string.Format("중복 접속으로 기존 유저 짜른다: UserID:{0}, SessionID:{1}", resData.UserID, sessionID), LOG_LEVEL.TRACE);
            //        ServerLogic.WriteFileLog(string.Format("중복 접속으로 기존 유저 짜른다. UserID:{0}, SessionID:{1}", resData.UserID, sessionID), LOG_LEVEL.INFO);
                                        
            //        ServerNetworkRef.DisConnect(sessionID);
            //    }


            //    (DevLog.IsEnable(LOG_STRING_TYPE.LOGIN)).IfTrue(() => DevLog.Write(string.Format("클라이언트에 로그인 결과 보냄: UserID:{0}, result:{1}, 길드 번호:{2}", resData.UserID, errorCode.ToString(), resData.GuildUnique), LOG_LEVEL.INFO));

            //    if (errorCode == ERROR_CODE.NONE)
            //    {
            //        ServerLogic.WriteFileLog(string.Format("로그인 성공: UserID:{0}, Session:{1}, 길드 번호:{2}", resData.UserID, packetData.SessionID, resData.GuildUnique), LOG_LEVEL.INFO);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //    ServerLogic.WriteFileLog(string.Format("로그인 답변 에러: {0}", ex.Message), LOG_LEVEL.DEBUG);
            //}
        }

        // 모든 채팅 서버에 이 유저의 접속을 끊어주기를 요청. 이 요청에 의한 답변을 받은 유저는 접속을 종료해야 한다.
        public void RequestOwnDisconnect(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("RequestOwnDisconnect 요청 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var reqData = JsonConvert.DeserializeObject<PKTReqOwnDisconnect>(packetData.JsonFormatData);

            //    var dbRequest = new DBReqOwnDisconnect() { UserID = reqData.UserID, AuthToken = reqData.AuthToken };
            //    RequestDBJob<DBReqOwnDisconnect>(dbRequest, PACKETID.NTF_DB_OWN_DISCONNECT, packetData.SessionID);


            //    var response = new PKTResOwnDisconnect()
            //    {
            //        Result = (short)ERROR_CODE.NONE
            //    };

            //    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            //    byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //    var sendData = PacketToBytes.Make(PACKETID.RES_OWN_DISCONNECT, bodyData);

            //    ServerNetworkRef.SendData(packetData.SessionID, sendData, PACKETID.RES_OWN_DISCONNECT);

            //    ServerLogic.WriteFileLog(string.Format("UserID:{0}, Session:{1}", user.UserID, packetData.SessionID), LOG_LEVEL.INFO);
            //    DevLog.Write("DB에 RequestOwnDisconnect 요청 보냄", LOG_LEVEL.TRACE);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }


        void ResponseLoginToClient(ERROR_CODE errorCode, string sessionID)
        {
            //var resLogin = new PKTResLogin()
            //{
            //    Result = (short)errorCode
            //};

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.RES_LOGIN, bodyData);

            //ServerNetworkRef.SendData(sessionID, sendData, PACKETID.RES_LOGIN);
        }
    }
}
