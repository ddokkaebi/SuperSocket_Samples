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
        
        public static int LobbyStartIndex       = 0;
        public static int MaxUserPerLobby       = 0;
        public static int LobbyCountPerThread   = 0;
        public static int LobbyThreadCount      = 0;

        public static int DBWorkerThreadCount = 0;
        
        public static List<Tuple<string, int>> RedisAddressList = new List<Tuple<string, int>>();


        public static void Setting()
        {
            ChatServerUniqueID  = Properties.Settings.Default.ChatServerUniqueID;
            LobbyStartIndex     = Properties.Settings.Default.LobbyStartIndex;
            MaxUserPerLobby     = Properties.Settings.Default.MaxUserPerLobby;
            LobbyCountPerThread = Properties.Settings.Default.LobbyCountPerThread;
            LobbyThreadCount    = Properties.Settings.Default.LobbyThreadCount;


            DBWorkerThreadCount = Properties.Settings.Default.DBWorkerThreadCount;

            foreach(var address in Properties.Settings.Default.RedisDBInfo)
            {
                var token = address.Split("_");
                RedisAddressList.Add(new Tuple<string, int>(token[0], token[1].ToInt32()));
            }
        }
    }
}
