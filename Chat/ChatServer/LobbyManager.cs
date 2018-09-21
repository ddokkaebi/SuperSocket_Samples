using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;

namespace ChatServer
{
    class LobbyManager
    {
        List<Lobby> LobbyList = new List<Lobby>();

        public void CreateLobby(int lobbyCount, int startIndex, int maxUserCount)
        {
            for (var i = 0; i < lobbyCount; ++i)
            {
                var lobyIndex = startIndex + i;

                var lobby = new Lobby();

                lobby.Init(lobyIndex, maxUserCount);

                LobbyList.Add(lobby);
            }
        }

        public List<Lobby> GetLobbyList(int beginIndex, int endIndex)
        {
            var lobbyList = new List<Lobby>();

            return LobbyList.FindAll(x => (x.Index >= beginIndex) && (x.Index < endIndex));
        }
    }
        
}
