using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;
using CSBaseLib;

namespace ChatServer
{
    //TODO 삭제 예정

        /*
    class Lobby
    {
        public int Index { get; private set; }
        public int Number { get; private set; }

        int MaxRoomCount = 0;
        List<Room> RoomList = new List<Room>();

        public void Init(int index, int number, int maxRoomCount)
        {
            Index = index;
            Number = number;
            MaxRoomCount = maxRoomCount;

            CreateRooms(maxRoomCount);
        }

        void CreateRooms(int maxRoomCount)
        {
            var startNumber = ChatServerEnvironment.RoomStartNumber;
            var maxUserCount = ChatServerEnvironment.RoomMaxUserCount;

            for (int i = 0; i < maxRoomCount; ++i)
            {
                var room = new Room();
                room.Init(i, (startNumber + i), maxUserCount);

                RoomList.Add(room);
            }
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
    */
}
