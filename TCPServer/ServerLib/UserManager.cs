using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace ServerLib
{
    public class UserManager
    {
        ServerNetwork ServerNetworkRef;

        Int64 UserSequenceNumber = 0;

        // 접속 유저 객체 Pool
        List<ConnectUser> UserPool = new List<ConnectUser>();
        // 접속 유저 객체 Pool에서 사용하지 않은 인덱스
        Queue<int> EmptyUserIndexPool = new Queue<int>();
                
        Dictionary<string, ConnectUser> UserIDMap = new Dictionary<string, ConnectUser>();
        Dictionary<string, ConnectUser> UserSessionMap = new Dictionary<string, ConnectUser>();


        UserGuildManager GuildManager = new UserGuildManager();
        UserStatusCheckOption StatusCheckOption;


        public void Init(ServerNetwork serverNetwork, int maxUserPoolSzie)
        {
            ServerNetworkRef = serverNetwork;

            for (int i = 0; i < maxUserPoolSzie; ++i)
            {
                var user = new ConnectUser();
                user.Init(i);

                UserPool.Add(user);
                EmptyUserIndexPool.Enqueue(i);
            }
        }
                
        public int UserCountOfPool() { return UserPool.Count(); }
        
        public int TotalConnectUserCount() { return UserSessionMap.Count();  }

        public ERROR_CODE AddUser(string sessionID)
        {
            if (UserSessionMap.ContainsKey(sessionID))
            {
                return ERROR_CODE.ADD_USER_DUPLICATION_SESSION;
            }


            var user = AllocateUserPool();
            if (user == null)
            {
                return ERROR_CODE.ADD_USER_FULL;
            }

            
            UserSequenceNumber += 1;
            var 현재시간 = CommonServerLib.Util.TimeTickToSec(DateTime.Now.Ticks);
            user.SetUse(UserSequenceNumber, sessionID, 현재시간);

            UserSessionMap.Add(sessionID, user);

            return ERROR_CODE.NONE;
        }

        public ERROR_CODE RemoveUser(string sessionID)
        {
            var user = GetUserSessionID(sessionID);

            if (user != null)
            {
                GuildManager.RemoveGuild(user);
                UserSessionMap.Remove(sessionID);
                UserIDMap.Remove(user.UserID);
                                
                DeAllocateUserPool(user);
            }

            return ERROR_CODE.NONE;
        }

        public ConnectUser GetUserSessionID(string sessionID)
        {
            if (string.IsNullOrEmpty(sessionID) || sessionID == "none")
            {
                return null;
            }

            ConnectUser user = null;
            UserSessionMap.TryGetValue(sessionID, out user);
            return user;
        }

        public ConnectUser GetUserID(string userID)
        {
            ConnectUser user = null;
            UserIDMap.TryGetValue(userID, out user);
            return user;
        }

        public ConnectUser GetUserPoolIndex(int poolIndex)
        {
            if (poolIndex < 0 || poolIndex >= UserPool.Count())
            {
                return null;
            }

            return UserPool[poolIndex];
        }

        public string GetSessionID(int userPoolIndex)
        {
            if (userPoolIndex < 0 || userPoolIndex >= UserPool.Count())
            {
                return null;
            }

            return UserPool[userPoolIndex].SessionID;
        }

        public ERROR_CODE 유저_인증_완료(string sessionID, string userID, string nickName, Int64 guildUnique)
        {
            var user = GetUserSessionID(sessionID);

            if (user == null)
            {
                return ERROR_CODE.USER_AUTH_SEARCH_FAILURE_USER_ID;
            }

            // 꼭 user.SetAuthorized 위에 실행해야 한다.
            if (UserIDMap.ContainsKey(userID))
            {
                return ERROR_CODE.ADD_USER_DUPLICATION_USERID;
            }

            if (user.SetAuthorized(userID, nickName) == false)
            {
                return ERROR_CODE.USER_AUTH_ALREADY_SET_AUTH;
            }

            UserIDMap.Add(userID, user);
            AddGuild(user, guildUnique);

            return ERROR_CODE.NONE;
        }


        public void SetUserStatusCheckOption(UserStatusCheckOption option)
        {
            StatusCheckOption = option;
        }
        

        // 유저 상태 조사
        public FAIL_USER_STATUS CheckUserStatus(int userIndex, Int64 curTimeSec)
        {
            if (userIndex < 0 || userIndex >= UserPool.Count() || UserPool[userIndex].IsUnUse())
            {
                return FAIL_USER_STATUS.NONE;
            }

            
            if (UserPool[userIndex].접속_종료_예약_시간_초 > 0 )
            {
                var diff = curTimeSec - UserPool[userIndex].접속_종료_예약_시간_초;
                if (diff >= 0)
                {
                    return FAIL_USER_STATUS.OVER_CLOSE_WAIT_TIME;
                }
            }

            // 지정 시간 내에 로그인 완료를 못한 유저 제거. 3분
            if (StatusCheckOption.CheckTimeLimitUserAuth && UserPool[userIndex].Authorized == false)
            {
                var diff = curTimeSec - UserPool[userIndex].접속_시간_초;
                if (diff > 180) 
                {
                    return FAIL_USER_STATUS.OVER_TIME_LOG_IN_COMPLETE;
                }
            }

            // 지정 시간 내에 로비에 들어가지 못한 유저 제거. 5분
            if (StatusCheckOption.CheckTimeLimitUserLobby && UserPool[userIndex].EnteredLobby() == false)
            {
                var diff = curTimeSec - UserPool[userIndex].로비_대기_시작_시간_초;
                if (diff > 300)
                {
                    return FAIL_USER_STATUS.OVER_TIME_LOBBY_IN_COMPLETE;
                }
            }

            // 지정 시간 내에 패킷을 보내지 않은 유저
            if (StatusCheckOption.CheckHeartBeat && UserPool[userIndex].EnteredLobby())
            {
                var diff = curTimeSec - UserPool[userIndex].최근_패킷_받은_시간_초;

                // 유저제거. 5분                
                if (diff > 300)
                {
                    return FAIL_USER_STATUS.FAIL_HEART_BEAT;
                }

                // 유저에게 서버에서 패킷 보내지 않음. 1분
                if (diff > 60)
                {
                    return FAIL_USER_STATUS.WAIT_HEART_BEAT;
                }
            }


            // 서버로 패킷을 너무 많이 보내는(DDos ?) 유저 
            if (StatusCheckOption.CheckHeavyNetworkTrafficUser && UserPool[userIndex].CheckHeavuRequest())
            {
                return FAIL_USER_STATUS.HEAVY_REQUEST_COUNT;
            }
                        
            return FAIL_USER_STATUS.NONE;
        }

        public void AddGuild(ConnectUser user, Int64 guildUnique)
        {
            user.SetGuildUnique(guildUnique);
            GuildManager.AddGuild(user);
        }

        public void GuildChatting(Int64 guildUnique, string nickName, string chatMsg)
        {
            GuildManager.GuildChatting(ServerNetworkRef, guildUnique, nickName, chatMsg);
        }



        /////////////////////////
        /// < 유저 객체풀 관련 >
        /////////////////////////
        ConnectUser AllocateUserPool()
        {
            if (EmptyUserIndexPool.IsEmpty())
            {
                return null;
            }

            var index = EmptyUserIndexPool.Dequeue();

            return UserPool[index];
        }

        void DeAllocateUserPool(ConnectUser user)
        {
            user.Clear();
            EmptyUserIndexPool.Enqueue(user.UserIndex);
        }
    }

    //struct DisConnUser
    //{
    //    public string SessionID;
    //    public DateTime Time;
    //    public DISCONNECT_REASON DisConnReason;
    //}
    
    //enum DISCONNECT_REASON : short
    //{
    //    NONE        = 0,
    //    USER_FULL   = 1,
    //}

    public enum FAIL_USER_STATUS
    {
        NONE = 0,
        OVER_TIME_LOG_IN_COMPLETE   = 1,
        OVER_TIME_LOBBY_IN_COMPLETE = 2,
        WAIT_HEART_BEAT             = 3,
        FAIL_HEART_BEAT             = 4,
        OVER_CLOSE_WAIT_TIME        = 5,
        HEAVY_REQUEST_COUNT         = 6,
    }
}
