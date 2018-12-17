using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServerLib
{
    public class ServerDefineData
    {
        public static int ChatServerStartID = 1;
        public static int ChatServerLastID = 1;

        public static void Setting(int chatServerStartID, int chatServerLastID)
        {
            ChatServerStartID = chatServerStartID;
            ChatServerLastID = chatServerLastID;
        }

        public const int WRONG_USER_CLOSE_WAIT_TIME_SEC = 5; // 잘못된 유저가 접속을 끊을 때까지 기다려주는 시간(초)

        public const int MAX_CLIENT_REQUEST_RECORD_COUNT = 5;
        public const int MAX_CLIENT_REQUEST_PER_SECOND = 10;
    }

    public enum LOG_LEVEL
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        DISABLE
    }

    public enum LOG_STRING_TYPE
    {
        CONNECTED = 0,
        DISCONNECTED,
        CHECK_USER_STATUS,
        PACKET_RECEIVED,
        SYSTEM_DISCONNECT_CLIENT,

        HEART_BEAT,
        BLOCK_USER,
        WRONG_USER,
        S2S_MESSAGE,
        LOGIN,
        LOBBY_ENTER,
        LOBBY_LEAVE,

        END
    }

}
