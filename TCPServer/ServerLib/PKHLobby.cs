using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using CommonServerLib;


namespace ServerLib
{
    class PKHLobby : PKBaseHandler
    {
        // 로비 들어가기 요청(from 클라이언트)
        public void RequestEnterLobby(ServerPacketData packetData, ConnectUser user)
        {
            //DevLog.Write("로그인 요청 받음", LOG_LEVEL.TRACE);
            
            //try
            //{
            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", packetData.SessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    if(user.EnteredLobby())
            //    {
            //        ResponseEnterLobbyToClient(ERROR_CODE.ENTER_LOBBY_ALREADY_ENTERED, packetData.SessionID);
            //        return;
            //    }

            //    var reqData = JsonConvert.DeserializeObject<PKTReqEnterLobby>(packetData.JsonFormatData);


            //    // 로비 인원이 다 찼는지 조사.
            //    var error = LobbyManagerRef.EnterLobby(reqData.LobbyID, user);

            //    if (error != ERROR_CODE.NONE)
            //    {
            //        ResponseEnterLobbyToClient(error, packetData.SessionID);
            //        return;
            //    }
                
            //    // DB에 유저의 로비 정보를 요청한다.
            //    var dbRequest = new DBReqEnterLobby() { UserID = user.UserID, LobbyID = reqData.LobbyID };
            //    RequestDBJob<DBReqEnterLobby>(dbRequest, PACKETID.REQ_DB_ENTER_LOBBY, packetData.SessionID);

            //    DevLog.Write("DB에 로비 들어가기 요청 보냄", LOG_LEVEL.TRACE);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        // 로비 들어가기 DB 처리 답변
        public void ResponseDBEnterLobby(ServerPacketData packetData, ConnectUser user)
        {
            //DevLog.Write("DB에서 로비입장 답변 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var resData = JsonConvert.DeserializeObject<DBResEnterLobby>(packetData.JsonFormatData);
            //    var errorCode = resData.Result;

            //    // 로비 입장 실패이면 로비에서 빼낸다.
            //    if (errorCode != ERROR_CODE.NONE)
            //    {
            //        LeaveLobby(LEAVE_LOBBY_TYPE.FAIL_REQUEST_ENTER, resData.LobbyID, resData.UserID, user);
            //    }
                
            //    if(user == null || user.LobbyID != resData.LobbyID)
            //    {
            //        LeaveLobby(LEAVE_LOBBY_TYPE.FAIL_REQUEST_ENTER, resData.LobbyID, resData.UserID, user);

            //        if (errorCode == ERROR_CODE.NONE)
            //        {
            //            errorCode = ERROR_CODE.ENTER_LOBBY_RE_CHECK_INVALID_DATA;
            //        }
            //    }

            //    if (errorCode == ERROR_CODE.NONE)
            //    {
            //        LobbyManagerRef.EnterLobbyComplete(resData.LobbyID, resData.UserID);

            //        var lobbyUserCount = LobbyManagerRef.CurrentUserCount(resData.LobbyID);
            //        InnerMessageHostProgram.UpdateLobbyInfo(resData.LobbyID, lobbyUserCount);

            //        (DevLog.IsEnable(LOG_STRING_TYPE.LOBBY_ENTER)).IfTrue(() => DevLog.Write(string.Format("로비입장 완료. LobbyID:{0}, UserID{{1}", resData.LobbyID, resData.UserID), LOG_LEVEL.INFO));

            //        ServerLogic.WriteFileLog(string.Format("로비 입장. LobbyID:{0}, UserID:{1}, Session:{2}", resData.LobbyID, resData.UserID, packetData.SessionID), LOG_LEVEL.INFO);
            //    }
                
            //    ResponseEnterLobbyToClient(errorCode, packetData.SessionID);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }
        
        // 로비 나가기 요청(from 클라이언트)
        public void RequestLeaveLobby(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("로비 나가기 요청 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var reqData = JsonConvert.DeserializeObject<PKTReqEnterLobby>(packetData.JsonFormatData);

            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", packetData.SessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    if (user.EnteredLobby() == false)
            //    {
            //        ResponseLeaveLobbyToClient(ERROR_CODE.LEAVE_LOBBY_DO_NOT_ENTER_LOBBY, packetData.SessionID);
            //        return;
            //    }

            //    LeaveLobby(LEAVE_LOBBY_TYPE.REQUEST, user.LobbyID, user.UserID, user);
                
            //    // DB에 유저의 로비 정보를 요청한다.
            //    var dbRequest = new DBReqLeaveLobby() { UserID = user.UserID, LobbyID = reqData.LobbyID };
            //    RequestDBJob<DBReqLeaveLobby>(dbRequest, PACKETID.REQ_DB_LEAVE_LOBBY, packetData.SessionID);

            //    DevLog.Write("DB에 로비 나가기 요청 보냄", LOG_LEVEL.TRACE);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        public void ResponseDBLeaveLobby(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("DB에서 로비 나가기 답변 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var resData = JsonConvert.DeserializeObject<DBResLeaveLobby>(packetData.JsonFormatData);
            //    var errorCode = resData.Result;

            //    if (user != null && user.UserID == resData.UserID)
            //    {
            //        ResponseLeaveLobbyToClient(errorCode, packetData.SessionID);
            //    }

            //    var lobbyUserCount = LobbyManagerRef.CurrentUserCount(resData.LobbyID);
            //    InnerMessageHostProgram.UpdateLobbyInfo(resData.LobbyID, lobbyUserCount);

            //    if (errorCode == ERROR_CODE.NONE)
            //    {
            //        (DevLog.IsEnable(LOG_STRING_TYPE.LOBBY_LEAVE)).IfTrue(() => DevLog.Write(string.Format("로비 나가기 완료. Lobby:{0}, ID:{1}", resData.LobbyID, resData.UserID), LOG_LEVEL.INFO));

            //        ServerLogic.WriteFileLog(string.Format("로비 나가기. LobbyID:{0}, UserID:{1}, Session:{2}", resData.LobbyID, resData.UserID, packetData.SessionID), LOG_LEVEL.INFO);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }


        // 로비 채팅 요청
        public void RequestLobbyChat(ServerPacketData packetData, ConnectUser user)
        {
            //DevLog.Write("로비 채팅 요청 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var reqData = JsonConvert.DeserializeObject<PKTReqLobbyChat>(packetData.JsonFormatData);

            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", packetData.SessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    if (user.EnteredLobby() == false)
            //    {
            //        var sendErrorData = MakeLobbyChatPacket(ERROR_CODE.LOBBY_CHAT_LOBBY_DO_NOT_ENTER_LOBBY, "none", "none");
            //        ServerNetworkRef.SendData(packetData.SessionID, sendErrorData, PACKETID.NTF_LOBBY_CHAT);
            //        return;
            //    }

            //    if (string.IsNullOrEmpty(reqData.ChatMsg) || string.IsNullOrWhiteSpace(reqData.ChatMsg) ||
            //        reqData.ChatMsg.Length > 80)
            //    {
            //        var sendErrorData = MakeLobbyChatPacket(ERROR_CODE.LOBBY_CHAT_INVALID_MESSAGE_LENGTH, "none", "none");
            //        ServerNetworkRef.SendData(packetData.SessionID, sendErrorData, PACKETID.NTF_LOBBY_CHAT);
            //        return;
            //    }
                           
     
            //    var sendChatData = MakeLobbyChatPacket(ERROR_CODE.NONE, user.NickName, reqData.ChatMsg);

            //    LobbyManagerRef.Chatting(user.LobbyID, sendChatData);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        // 길드 채팅 요청
        public void RequestGuildChat(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("길드 채팅 요청 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var reqData = JsonConvert.DeserializeObject<PKTReqGuildChat>(packetData.JsonFormatData);

            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", packetData.SessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    var result = CheckGuildChatting(user, reqData.Msg);
            //    if (result != ERROR_CODE.NONE)
            //    {
            //        var sendErrorData = MakeGuildChatPacket(result, "n", "n");
            //        ServerNetworkRef.SendData(packetData.SessionID, sendErrorData, PACKETID.NTF_GUILD_CHAT);
            //        return;
            //    }


            //    if (ChatServerEnvironment.ChatServerCount() != 1)
            //    {
            //        var db = new DBNtfGuildChat() { GU = user.GuildUnique, Name = user.NickName, Msg = reqData.Msg };
            //        RequestDBJob<DBNtfGuildChat>(db, PACKETID.NTF_DB_GUILD_CHAT, packetData.SessionID);
            //    }
            //    else
            //    {
            //        UserManagerRef.GuildChatting(user.GuildUnique, user.NickName, reqData.Msg);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        // 유저의 길드 정보 로딩 요청
        public void RequestLoadUserGuildInfo(ServerPacketData packetData, ConnectUser user)
        {
            DevLog.Write("유저 길드 정보 로딩 요청 받음", LOG_LEVEL.TRACE);

            //try
            //{
            //    var reqData = JsonConvert.DeserializeObject<PKTReqLoadUserGuildInfo>(packetData.JsonFormatData);

            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", packetData.SessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    if (user.ValidGuild())
            //    {
            //        var sendErrorData = MakeLoadUserGuildInfoPacket(ERROR_CODE.ALREADY_USER_GUILD_INFO_LOADED);
            //        ServerNetworkRef.SendData(packetData.SessionID, sendErrorData, PACKETID.RES_LOAD_USER_GUILD_INFO);
            //        return;
            //    }

            //    var db = new DBReqUserGuildInfo() { UserID = user.UserID };
            //    RequestDBJob<DBReqUserGuildInfo>(db, PACKETID.REQ_DB_LOAD_USER_GUILD_INFO, packetData.SessionID);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        public void ResponseLoadUserGuildInfo(ServerPacketData packetData, ConnectUser user)
        {
            //try
            //{
            //    var resData = JsonConvert.DeserializeObject<DBResUserGuildInfo>(packetData.JsonFormatData);
            //    var errorCode = resData.Result;

            //    if (user == null && user.UserID != resData.UserID)
            //    {
            //        return;
            //    }
                                
            //    if (errorCode == ERROR_CODE.NONE)
            //    {
            //        UserManagerRef.AddGuild(user, resData.GuildUnique);
            //    }

            //    var sendData = MakeLoadUserGuildInfoPacket(errorCode);
            //    ServerNetworkRef.SendData(packetData.SessionID, sendData, PACKETID.RES_LOAD_USER_GUILD_INFO);

            //    DevLog.Write(string.Format("유저ID:{0}, 길드번호:{1}", user.UserID, user.GuildUnique), LOG_LEVEL.INFO);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }





        //byte[] MakeLobbyChatPacket(ERROR_CODE errorCode, string nickName, string chatMsg)
        //{
        //    var resLogin = new PKTNtfLobbyChat()
        //    {
        //        Result = (short)errorCode,
        //        NickName = nickName,
        //        ChatMsg = chatMsg
        //    };

        //    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
        //    byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
        //    var sendData = PacketToBytes.Make(PACKETID.NTF_LOBBY_CHAT, bodyData);
        //    return sendData;
        //}

        void ResponseEnterLobbyToClient(ERROR_CODE errorCode, string sessionID)
        {
            //var resLogin = new PKTResEnterLobby()
            //{
            //    Result = (short)errorCode
            //};

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.RES_ENTER_LOBBY, bodyData);

            //ServerNetworkRef.SendData(sessionID, sendData, PACKETID.RES_ENTER_LOBBY);
        }

        void ResponseLeaveLobbyToClient(ERROR_CODE errorCode, string sessionID)
        {
            //var resLogin = new PKTResLeaveLobby()
            //{
            //    Result = (short)errorCode
            //};

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.RES_LEAVE_LOBBY, bodyData);

            //ServerNetworkRef.SendData(sessionID, sendData, PACKETID.RES_LEAVE_LOBBY);
        }

        ERROR_CODE CheckGuildChatting(ConnectUser user, string chatMsg)
        {
            if (user.EnteredLobby() == false)
            {
                return ERROR_CODE.GUILD_CHAT_DO_NOT_ENTER_LOBBY;
            }

            if (user.ValidGuild() == false)
            {
                return ERROR_CODE.GUILD_CHAT_INVALID_GUILD_UNIQUE;
            }

            if (chatMsg == null || string.IsNullOrWhiteSpace(chatMsg) || chatMsg.Length > 80)
            {
                return ERROR_CODE.GUILD_CHAT_INVALID_MESSAGE_LENGTH;
            }

            return ERROR_CODE.NONE;
        }

        public static byte[] MakeGuildChatPacket(ERROR_CODE errorCode, string nickName, string chatMsg)
        {
            //var resLogin = new PKTNtfGuildChat()
            //{
            //    Result = (short)errorCode,
            //    Name = nickName,
            //    Msg = chatMsg
            //};

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.NTF_GUILD_CHAT, bodyData);
            //return sendData;
            return null;
        }

        public static byte[] MakeLoadUserGuildInfoPacket(ERROR_CODE errorCode)
        {
            //var resLogin = new PKTResLoadUserGuildInfo() { Result = (short)errorCode };

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(resLogin);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.RES_LOAD_USER_GUILD_INFO, bodyData);
            //return sendData;
            return null;
        }
                
    } // class End
}
