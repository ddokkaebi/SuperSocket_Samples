using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;
using CSBaseLib;

namespace ChatServer
{
    class Lobby
    {
        public int Index { get; private set; }

        int MaxUserCount = 0;

        public void Init(int index, int maxUserCount)
        {
            Index = index;
            MaxUserCount = maxUserCount;
        }

        public ERROR_CODE AddUser(int lobbyIndex, LobbyUser lobbyUser )
        {
            return ERROR_CODE.NONE;
        }

        public ERROR_CODE RemoveUser(int lobbyIndex, int lobbyUserIndex)
        {
            return ERROR_CODE.NONE;
        }

        public ERROR_CODE Chatting(int lobbyIndex, string chatMsg)
        {
            return ERROR_CODE.NONE;
        }
    }
}
