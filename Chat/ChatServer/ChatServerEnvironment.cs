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
        
        public static int RoomMaxCountPerThread = 0;
        public static int RoomThreadCount      = 0;
        public static int RoomMaxUserCount = 0;
        public static int RoomStartNumber = 0;

        public static int DBWorkerThreadCount = 0;

        public static string RedisAddress;


        public static void Setting()
        {
            ChatServerUniqueID  = Properties.Settings.Default.ChatServerUniqueID;

            RoomMaxCountPerThread = Properties.Settings.Default.RoomMaxCountPerThread;
            RoomThreadCount    = Properties.Settings.Default.RoomThreadCount;
            RoomMaxUserCount = Properties.Settings.Default.RoomMaxUserCount;
            RoomStartNumber = Properties.Settings.Default.RoomStartNumber;

            DBWorkerThreadCount = Properties.Settings.Default.DBWorkerThreadCount;

            RedisAddress = Properties.Settings.Default.RedisDBInfo;
        }
    }
}
