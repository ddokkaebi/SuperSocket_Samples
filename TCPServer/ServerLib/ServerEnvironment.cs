using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    public class ServerEnvironment
    {
        public static int ServerType { get; private set; }

        // 이 채팅 서버의 이름
        public static string UniqueName { get; private set; }

        // 채팅 서버에서 관리할 수 있는 최대 유저 수
        public static int MaxUserCount { get; private set; }

        

        // 채팅 서버에서 관리할 로비의 시작 번호
        public static int LobbyStartIndex { get; private set; }

        // 로비 수
        public static int LobbyCount { get; private set; }

        // 로비 당 최대 유저 수
        public static int MaxUserPerLobby { get; private set; }

        // DB 처리 스레드 수
        public static int DBWorkerThreadCount { get; private set; }
        
        // 접속할 Redis 서버 주소 리스트
        public static List<Tuple<string, int>> GameRedisList { get; private set; }
        public static List<Tuple<string, int>> ChatRedisList { get; private set; }

        public static ChatServerConfig ChatServer { get; private set; }


        public static void SetBasic(int type, string name, int maxUserCount, int dbWorkerThreadCount)
        {
            ServerType = type;
            UniqueName = name;
            MaxUserCount = maxUserCount;
            DBWorkerThreadCount = dbWorkerThreadCount;
        }

        public static void SetDB()
        {
        }

        public static void SetLobby(int startIndex, int maxCount, int maxUserCountPerLobby)
        {
            LobbyStartIndex = startIndex;
            LobbyCount = maxCount;
            MaxUserPerLobby = maxUserCountPerLobby;
        }

        public static void SetChatServer(int uniqueID, int startID, int lastID)
        {
            if (ChatServer == null)
            {
                ChatServer = new ChatServerConfig();
            }

            ChatServer.Set(uniqueID, startID, lastID);
        }

        // 서버간 메시지를 읽어올 때 최대 개수. 이것보다 많으면 다 삭제한다.
        //public static int MaxS2SMessageReadCount { get; private set; }
    }

    public class ChatServerConfig
    {
        public void Set(int uniqueID, int startID, int lastID)
        {
            UniqueID = uniqueID;
            StartID = startID;
            LastID = lastID;
            Count = (LastID - StartID) + 1;
        }

        // 채팅 서버의 ID
        public int UniqueID { get; private set; }

        // 모든 채팅 서버 ID 시작과 끝 번호
        public int StartID { get; private set; }
        public int LastID { get; private set; }
        
        public int Count { get; private set; }
    }

    public enum SERVER_TYPE
    {
        NONE = 0,
        CHAT = 1,
    }
}
