using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Util = CommonServerLib.Util;
using CSBaseLib;

namespace ServerLib
{
    // 전체 로비들 관리 클래스
    public class LobbyManager
    {
        ServerNetwork ServerNetworkRef;
        List<Lobby> LobbyList = new List<Lobby>();


        public void CreateLobby(ServerNetwork serverNetwork, int lobbyCount, int startIndex, int maxUserCount)
        {
            ServerNetworkRef = serverNetwork;

            for (var i = 0; i < lobbyCount; ++i)
            {
                var lobyIndex = startIndex + i;

                var lobby = new Lobby();

                lobby.Init(lobyIndex, maxUserCount);

                LobbyList.Add(lobby);
            }
        }

        public int TotalLobbyCount() { return LobbyList.Count(); }

        public Lobby GetLobby(int lobbyID)
        {
            foreach(var lobby in LobbyList)
            {
                if(lobby.ID == lobbyID)
                {
                    return lobby;
                }
            }

            return null;
        }

        public Lobby LobbyRef(int lobbyIndex)
        {
            if (lobbyIndex < 0 || lobbyIndex >= LobbyList.Count())
            {
                return null;
            }

            return LobbyList[lobbyIndex];
        }

        public int CurrentUserCount(int lobbyID)
        {
            var lobby = GetLobby(lobbyID);
            if(lobby == null)
            {
                return -1;
            }

            return lobby.CurrentUserCount();
        }

        public List<Lobby> GetLobbyList(int beginIndex, int endIndex)
        {
            var lobbyList = new List<Lobby>();

            return LobbyList.FindAll(x => (x.ID >= beginIndex) && (x.ID < endIndex));
        }

        public string AllLobbyUserCountStringFormatForRedis()
        {
            List<int> countList = new List<int>();
            LobbyList.ForEach(x => countList.Add(x.CurrentUserCount()));

            string userCount = string.Join("_", countList.ToArray());
            return userCount;
        }

        public ERROR_CODE EnterLobby(int lobbyID, ConnectUser user)
        {
            var error = ERROR_CODE.NONE;

            var lobby = GetLobby(lobbyID);
            if (lobby == null)
            {
                return ERROR_CODE.ENTER_LOBBY_INVALID_LOBBY_ID;
            }

            error = lobby.AddUser(user);
            if (error == ERROR_CODE.NONE)
            {
                user.EnterLobby(lobbyID, Util.TimeTickToSec(DateTime.Now.Ticks));
            }

            return error;
        }

        public void EnterLobbyComplete(int lobbyID, string userID)
        {
            // 앞에서 이미 검증을 한번 했기 때문에 여기서의 실패는 없다고 봐도 되기 때문에 에러코드 반환을 하지 않는다.
            var lobby = GetLobby(lobbyID);
            if (lobby == null)
            {
                return ;
            }

            lobby.EnterLobbyComplete(userID);
        }

        public void LeaveLobby(LEAVE_LOBBY_TYPE leaveType, int lobbyID, string userID, ConnectUser user)
        {
            var lobby = GetLobby(lobbyID);
            if (lobby == null)
            {
                return;
            }

            lobby.RemoveUser(userID);

            if(user != null)
            {
                user.LeaveLobby(Util.TimeTickToSec(DateTime.Now.Ticks));
            }
        }

        public ERROR_CODE Chatting(int lobbyID, byte[] sendChatData)
        {
            var lobby = GetLobby(lobbyID);
            if (lobby == null)
            {
                return ERROR_CODE.LOBBY_CHAT_INVALID_LOBBY_ID;
            }

            lobby.Chatting(sendChatData, ServerNetworkRef);

            return ERROR_CODE.NONE;
        }

        public bool ServerNotification(int lobbyIndex, string notifyMessage)
        {
            var lobby = LobbyRef(lobbyIndex);

            if (lobby == null)
            {
                return false;
            }

            if (lobby.CurrentUserCount() < 1)
            {
                return false;
            }

            if (ServerNetworkRef != null)
            {
                lobby.ServerNotification(notifyMessage, ServerNetworkRef);
            }


            //CommonServerLib.DevLog.Write(string.Format("로비 전체 메시지 보내기. LobbyID:{0}, Msg:{1}", lobby.ID, notifyMessage), CommonServerLib.LOG_LEVEL.INFO);
            return true;
        }
    }
        
}
