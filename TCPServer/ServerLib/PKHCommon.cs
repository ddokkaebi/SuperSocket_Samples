using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using CommonServerLib;



namespace ServerLib
{
    public class PKHCommon : PKBaseHandler
    {
        int 가장_최근에_조사한_유저_인덱스 = 0;
        BroadcastMessagManager AllUserNotifyMessage = null;

        
        public void CreateBroadcastMessagManager(LobbyManager lobbyManager, int 한번에_보낼_로비_수)
        {
            AllUserNotifyMessage = new BroadcastMessagManager();
            AllUserNotifyMessage.Init(lobbyManager, 한번에_보낼_로비_수);
        }
                        
        public void SystemConnectClient(ServerPacketData requestData, ConnectUser user)
        {
            var result = UserManagerRef.AddUser(requestData.SessionID);

            if (result == ERROR_CODE.NONE)
            {
                MsgToHostProgram.CurrentUserCount(ServerNetworkRef.SessionCount);
            }
            else
            {
                NotifyDisConnectToClient(result, requestData.SessionID);
            }

            FileLogger.Write(string.Format("클라이언트 Connect. Session:{0}", requestData.SessionID), LOG_LEVEL.INFO);
        }

        public void SystemDisConnectClient(ServerPacketData requestData, ConnectUser user)
        {
            IfLobbyInUserThenLeaveLobby(requestData.SessionID);

            UserManagerRef.RemoveUser(requestData.SessionID);

            MsgToHostProgram.CurrentUserCount(ServerNetworkRef.SessionCount);

            (DevLog.IsEnable(LOG_STRING_TYPE.SYSTEM_DISCONNECT_CLIENT)).IfTrue(() => DevLog.Write(string.Format("클라이언트 Disconnect. Session:{1}", requestData.SessionID), LOG_LEVEL.INFO));
            FileLogger.Write(string.Format("클라이언트 Disconnect. Session:{0}", requestData.SessionID), LOG_LEVEL.INFO);
        }

        // 로비 정보 업데이트
        public void SystemLobbyInfoUpdate(ServerPacketData requestData, ConnectUser user)
        {
            requestData = null;

            var dbRequest = new DBReqRedisWriteString() { 
                UseRedisType = REDIS_TYPE.GAME,
                Key = ServerEnvironment.UniqueName, 
                Value = LobbyManagerRef.AllLobbyUserCountStringFormatForRedis() 
            };
            RequestDBJob<DBReqRedisWriteString>(dbRequest, PACKETID.REQ_EXECUTE_DB_SAVE_STRING_VALUE, "none");
        }

        // 서버간 메시지를 읽는다.
        public void SystemReadServerMessage(ServerPacketData requestData, ConnectUser user)
        {
            requestData = null;

            var key = string.Format("ChatServer_{0}_Bus", ServerEnvironment.ChatServer.UniqueID);
            var dbRequest = new DBReqRedisReadValue() { Key = key };
            RequestDBJob<DBReqRedisReadValue>(dbRequest, PACKETID.REQ_DB_READ_S2S_MESSAGE, "none");
        }

        // DB를 통해서 읽은 서버 메시지를 처리한다.
        public void ResponseDBReadS2SMessage(ServerPacketData packetData, ConnectUser user)
        {
            //DevLog.Write("DB 서버간 메시지 가져옴", LOG_LEVEL.TRACE);

            //try
            //{
            //    var resData = JsonConvert.DeserializeObject<DBResReadS2SMessage>(packetData.JsonFormatData);

            //    if (resData.MessageList == null || resData.MessageList.Count() < 1)
            //    {
            //        return;
            //    }

            //    resData.MessageList.ForEach(msg => 
            //        {
            //            (DevLog.IsEnable(LOG_STRING_TYPE.S2S_MESSAGE)).IfTrue(() => DevLog.Write(string.Format("DB 서버간 메시지:{0}-{1}", msg.Type, msg.Message), LOG_LEVEL.INFO));

            //            if (msg.From == "GAME") // 게임 서버에서 보낸 메시지
            //            {
            //                if (msg.Type == "NTF") // 공지, 알림
            //                {
            //                    AddAllUserNotifyMessage(msg.Message);
            //                }
            //            }
            //            else if (msg.From == "CHAT") // 다른 채팅 서버에서 보낸 메시지
            //            {
            //                ProceS2SMessageChatServer(msg);
            //            }
            //        }
            //      );
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }
        
        public void RequestHeartBeat(ServerPacketData packetData, ConnectUser user)
        {
            (DevLog.IsEnable(LOG_STRING_TYPE.HEART_BEAT)).IfTrue(() => DevLog.Write(string.Format("허트비트 요청 받음. ClientSession:{0}", packetData.SessionID), LOG_LEVEL.TRACE));

            //try
            //{
            //    var sessionID = packetData.SessionID;
                
            //    if (user == null)
            //    {
            //        DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", sessionID), LOG_LEVEL.TRACE);
            //        return;
            //    }

            //    user.SetPacketReceiveTimeSec(DateTime.Now.Ticks);

            //    var responseData = new PKTResHeartBeat()
            //    {
            //        DummyValue = 1
            //    };

            //    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
            //    byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //    var sendData = PacketToBytes.Make(PACKETID.RES_HEART_BEAT, bodyData);

            //    ServerNetworkRef.SendData(sessionID, sendData, PACKETID.RES_HEART_BEAT);
            //}
            //catch (Exception ex)
            //{
            //    // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
            //    DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            //}
        }

        // 접속한 유저 상태 조사.
        public void SystemCheckUserStatus(ServerPacketData packetData, ConnectUser user)
        {
            const int MAX_CHECK_COUNT = 256; // 한번에 최대 246인까지 한다.
            var totalUserCount = UserManagerRef.UserCountOfPool();

            (가장_최근에_조사한_유저_인덱스 >= totalUserCount).IfTrue(() => 가장_최근에_조사한_유저_인덱스 = 0);
            
            int maxCheckUserIndex = 가장_최근에_조사한_유저_인덱스 + MAX_CHECK_COUNT;
            (maxCheckUserIndex >= totalUserCount).IfTrue(() => maxCheckUserIndex = totalUserCount);
            
            Int64 curTimeSec = CommonServerLib.Util.TimeTickToSec(DateTime.Now.Ticks);

            for( int i = 가장_최근에_조사한_유저_인덱스; i < maxCheckUserIndex; ++i)
            {
                var result = UserManagerRef.CheckUserStatus(i, curTimeSec);
                
                if(result == FAIL_USER_STATUS.NONE)
                {
                    continue;
                }
                
                if (result == FAIL_USER_STATUS.WAIT_HEART_BEAT)
                {
                    var failUser = UserManagerRef.GetUserPoolIndex(i);
                    failUser.AbnormalNetwork();
                }
                else
                {
                    var sessionID = UserManagerRef.GetSessionID(i);
                    if (string.IsNullOrEmpty(sessionID) == false)
                    {
                        var msg = string.Format("session:{0}, DisCon resone:{1}", sessionID, result.ToString());
                        FileLogger.Write(msg, LOG_LEVEL.INFO);

                        ServerNetworkRef.DisConnect(sessionID);
                    }
                }

                (DevLog.IsEnable(LOG_STRING_TYPE.CHECK_USER_STATUS)).IfTrue(() => DevLog.Write(string.Format("UserPoolIndex: {0}, 상태이상: {1}. {2}", i, result.ToString(), DateTime.Now), LOG_LEVEL.DEBUG));
            }

            가장_최근에_조사한_유저_인덱스 += MAX_CHECK_COUNT;
        }

        // 잘못된 유저 처리.
        public void SystemWrongUser(ServerPacketData packetData, ConnectUser user)
        {
            try
            {
                var sessionID = packetData.SessionID;

                if (user == null)
                {
                    DevLog.Write(string.Format("세션ID로 유저 검색 실패. {0}", sessionID), LOG_LEVEL.TRACE);
                    return;
                }

                var 접속종료시간 = Util.TimeTickToSec(DateTime.Now.Ticks) + ServerDefineData.WRONG_USER_CLOSE_WAIT_TIME_SEC;
                user.SetWrongUser(접속종료시간);
                
                NotifyDisConnectToClient(ERROR_CODE.WRONG_USER, sessionID);

                (DevLog.IsEnable(LOG_STRING_TYPE.WRONG_USER)).IfTrue(() => DevLog.Write(string.Format("비정상 유저 처리. type:{0}, sessinID:{1}", packetData.JsonFormatData, sessionID), LOG_LEVEL.INFO));
            }
            catch (Exception ex)
            {
                // 패킷 해제에 의해서 로그가 남지 않도록 로그 수준을 Debug로 한다.
                DevLog.Write(ex.ToString(), LOG_LEVEL.DEBUG);
            }
        }

        public void SystemBroadcastMessag(ServerPacketData packetData, ConnectUser user)
        {
            AllUserNotifyMessage.SendMessage();
            //DevLog.Write("SystemBroadcastMessag", LOG_LEVEL.TRACE);
        }
        
        // 호스트 프로그램 UI 갱신
        public void SystemUIUpdate(ServerPacketData packetData, ConnectUser user)
        {
            var userCount = UserManagerRef.TotalConnectUserCount();
            var lobbyUserCount = LobbyManagerRef.AllLobbyUserCountStringFormatForRedis();
            MsgToHostProgram.UIUpdateInfo(userCount.ToString(), lobbyUserCount);
        }

        // 모든 유저에게 보낼 메시지를 저장한다.
        public void AddAllUserNotifyMessage(string message)
        {
            AllUserNotifyMessage.PushMessage(message);
        }

        // 만약 로비에 있었던 유저라면 로비를 나가도록 한다.
        void IfLobbyInUserThenLeaveLobby(string sessionID)
        {
            var user = UserManagerRef.GetUserSessionID(sessionID);
            if (user == null )
            {
                return;
            }

            var lobbyID = user.OwnDisconnectUserLobbyId();
            if(lobbyID != 0)
            {
                LeaveLobby(LEAVE_LOBBY_TYPE.DISCONNECT, lobbyID, user.UserID, user);
            }

            if (user.EnteredLobby())
            {
                LeaveLobby(LEAVE_LOBBY_TYPE.DISCONNECT, user.LobbyID, user.UserID, user);
            }
        }

        void ProceS2SMessageChatServer(S2SMessageData msg)
        {
            //if (msg.Type == S2S_MESSAGE_TYPE.DIS_CONNECT.ToString()) // 접속 해지
            //{
            //    var s2sMsg = JsonConvert.DeserializeObject<S2SMsgDisConnect>(msg.Message);

            //    var DisConnUser = UserManagerRef.GetUserID(s2sMsg.UserID);
            //    if (DisConnUser != null)
            //    {
            //        ServerLogic.WriteFileLog(string.Format("현재 서버에 접속되어 있는 유저. UserID:{0}", DisConnUser.SessionID), LOG_LEVEL.INFO);

            //        DisConnUser.OwnDisconnectUserInfo(s2sMsg.LobbyID);
            //        ServerNetworkRef.DisConnect(DisConnUser.SessionID);
            //    }
            //    else
            //    {
            //        if (LobbyManagerRef.GetLobby(s2sMsg.LobbyID) != null)
            //        {
            //            ServerLogic.WriteFileLog(string.Format("로비에서 나간다. UserID:{0}, LobbyID:{1}", s2sMsg.UserID, s2sMsg.LobbyID), LOG_LEVEL.INFO);

            //            base.LeaveLobby(LEAVE_LOBBY_TYPE.DISCONNECT, s2sMsg.LobbyID, s2sMsg.UserID, null);
            //        }
            //    }
            //}
            //else if (msg.Type == S2S_MESSAGE_TYPE.GUILD_CHAT.ToString()) // 길드 채팅
            //{
            //    var s2sMsg = JsonConvert.DeserializeObject<S2SMsgGuildChat>(msg.Message);
            //    UserManagerRef.GuildChatting(s2sMsg.GU, s2sMsg.Name, s2sMsg.Msg);
            //}
        }
    }
}
