using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ChatServerEnvironment
    {
        public static int ChatServerUniqueID    = 0;
        
        public static int LobbyStartNumber       = 0;
        public static int MaxRoomCountPerLobby       = 0;
        public static int LobbyCountPerThread   = 0;
        public static int LobbyThreadCount      = 0;
        public static int RoomMaxUserCount = 0;
        public static int RoomStartNumber = 0;

        public static int DBWorkerThreadCount = 0;

        public static string RedisAddress;


        public static void Setting()
        {
            ChatServerUniqueID  = Properties.Settings.Default.ChatServerUniqueID;
            LobbyStartNumber     = Properties.Settings.Default.LobbyStartNumber;
            MaxRoomCountPerLobby     = Properties.Settings.Default.MaxRoomCountPerLobby;
            LobbyCountPerThread = Properties.Settings.Default.LobbyCountPerThread;
            LobbyThreadCount    = Properties.Settings.Default.LobbyThreadCount;
            RoomMaxUserCount = Properties.Settings.Default.RoomMaxUserCount;
            RoomStartNumber = Properties.Settings.Default.RoomStartNumber;

            DBWorkerThreadCount = Properties.Settings.Default.DBWorkerThreadCount;

            RedisAddress = Properties.Settings.Default.RedisDBInfo;
        }
    }
}
