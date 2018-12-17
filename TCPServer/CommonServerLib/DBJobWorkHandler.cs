using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CSBaseLib;
using Newtonsoft.Json;

namespace CommonServerLib
{
    public class DBJobWorkHandler
    {
        RedisLib RedisDBRef = null;
        
        public Tuple<ERROR_CODE, string> Init(RedisLib redis)
        {
            try
            {
                RedisDBRef = redis;

                // 미리 Redis와 연결이 되도록 여기서 더미 데이터를 요청한다.
                RedisDBRef.GetString(REDIS_TYPE.GAME, "test");
                RedisDBRef.GetString(REDIS_TYPE.CHAT, "test");
                
                return new Tuple<ERROR_CODE,string>(ERROR_CODE.NONE, "");
            }
            catch(Exception ex)
            {
                return new Tuple<ERROR_CODE, string>(ERROR_CODE.REDIS_INIT_FAIL, ex.ToString());
            }
        }

        public DBResultQueue RequestLogin(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqLogin>(dbQueue.JsonFormatData);
            var resLoginData = new DBResLogin() { UserID = reqData.UserID };

            try
            {
                var userInfo = RedisDBRef.GetUserInfo(reqData.UserID);
                if (userInfo.UID == 0 || userInfo.Token != reqData.AuthToken)
                {
                    resLoginData.Result = ERROR_CODE.DB_LOGIN_INVALID_AUTHTOKEN;
                    return SendResponseLogin(dbQueue.SessionID, resLoginData);
                }

                var nameAndguild = RedisDBRef.GetUserNameGuildUnique(reqData.UserID);

                if (string.IsNullOrEmpty(nameAndguild.Name) || nameAndguild.Name == "0")
                {
                    resLoginData.Result = ERROR_CODE.DB_LOGIN_INVALID_AUTHTOKEN;
                    return SendResponseLogin(dbQueue.SessionID, resLoginData);
                }

                resLoginData.GuildUnique = nameAndguild.GU;
                resLoginData.NickName = nameAndguild.Name;
                resLoginData.Result = ERROR_CODE.NONE;
                return SendResponseLogin(dbQueue.SessionID, resLoginData);
            }
            catch
            {
                resLoginData.Result = ERROR_CODE.DB_LOGIN_EXCEPTION;
                return SendResponseLogin(dbQueue.SessionID, resLoginData);
            }
        }

        DBResultQueue SendResponseLogin(string sessionID, DBResLogin dbResLogin)
        {
            var returnData = new DBResultQueue()
            {
                PacketID = PACKETID.RES_DB_LOGIN,
                SessionID = sessionID,
            };

            returnData.JsonFormatData = Newtonsoft.Json.JsonConvert.SerializeObject(dbResLogin);
            return returnData;
        }
        
        public DBResultQueue RequestEnterLobby(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqEnterLobby>(dbQueue.JsonFormatData);

            try
            {
                var tempLobbyID = GetUserLobbyIDFromRedis(reqData.UserID);
                if (tempLobbyID > 0)
                {
                    return SendResponseEnterLobby(ERROR_CODE.DB_ENTER_LOBBY_ALREADY_ENTERED, dbQueue.SessionID, reqData);
                }

                SetUserLobbyIDToRedis(reqData.UserID, reqData.LobbyID, false);

                // 한번 더 확인
                tempLobbyID = GetUserLobbyIDFromRedis(reqData.UserID);
                if(tempLobbyID != reqData.LobbyID)
                {
                    SetUserLobbyIDToRedis(reqData.UserID, 0, true);
                    return SendResponseEnterLobby(ERROR_CODE.DB_ENTER_LOBBY_DUPLICATION, dbQueue.SessionID, reqData);
                }

                return SendResponseEnterLobby(ERROR_CODE.NONE, dbQueue.SessionID, reqData);
            }
            catch
            {
                return SendResponseEnterLobby(ERROR_CODE.DB_ENTER_LOBBY_EXCEPTION, dbQueue.SessionID, reqData);
            }
        }

        DBResultQueue SendResponseEnterLobby(ERROR_CODE result, string sessionID, DBReqEnterLobby dbReqLogin)
        {
            var returnData = new DBResultQueue()
            {
                PacketID = PACKETID.RES_DB_ENTER_LOBBY,
                SessionID = sessionID,
            };

            var resLoginData = new DBResEnterLobby() { UserID = dbReqLogin.UserID, LobbyID = dbReqLogin.LobbyID, Result = result };
            returnData.JsonFormatData = Newtonsoft.Json.JsonConvert.SerializeObject(resLoginData);
            return returnData;
        }

        public DBResultQueue RequestLeaveLobby(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqLeaveLobby>(dbQueue.JsonFormatData);

            try
            {
                SetUserLobbyIDToRedis(reqData.UserID, 0, true);
                                
                return SendResponseLeaveLobby(ERROR_CODE.NONE, dbQueue.SessionID, reqData);
            }
            catch
            {
                return SendResponseLeaveLobby(ERROR_CODE.DB_ENTER_LOBBY_EXCEPTION, dbQueue.SessionID, reqData);
            }
        }

        DBResultQueue SendResponseLeaveLobby(ERROR_CODE result, string sessionID, DBReqLeaveLobby dbReqLogin)
        {
            var returnData = new DBResultQueue()
            {
                PacketID = PACKETID.RES_DB_LEAVE_LOBBY,
                SessionID = sessionID,
            };

            var resLoginData = new DBResLeaveLobby() { UserID = dbReqLogin.UserID, LobbyID = dbReqLogin.LobbyID, Result = result };
            returnData.JsonFormatData = Newtonsoft.Json.JsonConvert.SerializeObject(resLoginData);
            return returnData;
        }

        public DBResultQueue RequestExecDBSaveStringValue(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqRedisWriteString>(dbQueue.JsonFormatData);
            DBResultQueue resulteValue = new DBResultQueue();

            try
            {
                RedisDBRef.SetString(reqData.UseRedisType, reqData.Key, reqData.Value);
                return resulteValue;
            }
            catch
            {
                return resulteValue;
            }
        }

        public DBResultQueue RequestDBReadS2SMessage(DBQueue dbQueue)
        {
            return default(DBResultQueue);
        }
        
        public DBResultQueue NotifyOwnDisconnect(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqOwnDisconnect>(dbQueue.JsonFormatData);
            DBResultQueue resulteValue = new DBResultQueue();

            try
            {
                var userInfo = RedisDBRef.GetUserInfo(reqData.UserID);
                if (userInfo.UID == 0 || userInfo.Token != reqData.AuthToken)
                {
                    return resulteValue;
                }

                var s2sMsgData = new S2SMsgDisConnect()
                {
                    UserID = reqData.UserID,
                    LobbyID = GetUserLobbyIDFromRedis(reqData.UserID),
                };

                DBProcessor.WriteFileLog(string.Format("UserID:{0}, LobbyID:{1}", s2sMsgData.UserID, s2sMsgData.LobbyID), LOG_LEVEL.INFO);

                string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(s2sMsgData);
                SendMessageToChatServers(S2S_MESSAGE_TYPE.DIS_CONNECT.ToString(), jsonstring);
                                
                return resulteValue;
            }
            catch
            {
                return resulteValue;
            }
        }

        public DBResultQueue NotifyGuildChat(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBNtfGuildChat>(dbQueue.JsonFormatData);
            DBResultQueue resulteValue = new DBResultQueue();

            try
            {
                var s2sMsgData = new S2SMsgGuildChat()
                {
                    GU = reqData.GU,
                    Name = reqData.Name,
                    Msg = reqData.Msg,
                };

                string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(s2sMsgData);
                SendMessageToChatServers(S2S_MESSAGE_TYPE.GUILD_CHAT.ToString(), jsonstring);
                
                return resulteValue;
            }
            catch
            {
                return resulteValue;
            }
        }

        public void SendMessageToChatServers(string type, string sendMsg)
        {
            var value = new ChatToChatMessage
            {
                Type = type,
                ST = Util.TimeTickToSec(DateTime.Now.Ticks),
                Msg = sendMsg
            };
            
            for (int i = ServerDefineData.ChatServerStartID; i <= ServerDefineData.ChatServerLastID; ++i)
            {
                var key = string.Format("ChatServer_{0}_Bus", i);
                RedisDBRef.AddStringObjectList<ChatToChatMessage>(REDIS_TYPE.CHAT, key, value);
            }
        }

        public static string UserLobbyIDKey(string userID) { return userID + "_LB#"; }

        int GetUserLobbyIDFromRedis(string userID)
        {
            var key = UserLobbyIDKey(userID);
            var lobbyID = RedisDBRef.GetInt(REDIS_TYPE.CHAT, key);
            return lobbyID;
        }

        void SetUserLobbyIDToRedis(string userID, int lobbyID, bool async)
        {
            var key = UserLobbyIDKey(userID);
            RedisDBRef.SetInt(REDIS_TYPE.CHAT, key, lobbyID, async);
        }


        public DBResultQueue RequestLoadUserGuildInfo(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqUserGuildInfo>(dbQueue.JsonFormatData);
            var resData = new DBResUserGuildInfo() { Result = ERROR_CODE.NONE, UserID = reqData.UserID };

            try
            {
                var nameAndGuild = RedisDBRef.GetUserNameGuildUnique(reqData.UserID);
                if (nameAndGuild.GU == 0)
                {
                    resData.Result = ERROR_CODE.DB_LOAD_USER_GUILD_INFO_INVALID_USER_ID;
                    return MakeResponseLoadUserGuildInfo(dbQueue.SessionID, resData);
                }

                resData.GuildUnique = nameAndGuild.GU;
                return MakeResponseLoadUserGuildInfo(dbQueue.SessionID, resData);
            }
            catch
            {
                resData.Result = ERROR_CODE.DB_LOAD_USER_GUILD_INFO_EXCEPTION;
                return MakeResponseLoadUserGuildInfo(dbQueue.SessionID, resData);
            }
        }

        DBResultQueue MakeResponseLoadUserGuildInfo(string sessionID, DBResUserGuildInfo dbResData)
        {
            var returnData = new DBResultQueue()
            {
                PacketID = PACKETID.RES_DB_LOAD_USER_GUILD_INFO,
                SessionID = sessionID,
            };

            returnData.JsonFormatData = Newtonsoft.Json.JsonConvert.SerializeObject(dbResData);
            return returnData;
        }

        
    } // DBJobWorkHandler End
}
