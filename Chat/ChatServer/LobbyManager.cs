using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;

namespace ChatServer
{
    //TODO 삭제 예정

        /*
    class LobbyManager
    {
        List<Lobby> LobbyList = new List<Lobby>();

        public void CreateLobby()
        {
            //var lobbyCount = ChatServerEnvironment.RoomThreadCount * ChatServerEnvironment.LobbyCountPerThread;
            //var lobbyStartNumber = ChatServerEnvironment.LobbyStartNumber;
            //var maxRoomCount = ChatServerEnvironment.MaxRoomCountPerLobby;

            //for (var i = 0; i < lobbyCount; ++i)
            //{
            //    var startNumber = lobbyStartNumber + i;

            //    var lobby = new Lobby();
            //    lobby.Init(i, startNumber, maxRoomCount);
            //    LobbyList.Add(lobby);
            //}
        }

        public List<Lobby> GetLobbyList(int beginIndex, int endIndex)
        {
            var lobbyList = new List<Lobby>();

            return LobbyList.FindAll(x => (x.Index >= beginIndex) && (x.Index < endIndex));
        }
    }
        */
}
