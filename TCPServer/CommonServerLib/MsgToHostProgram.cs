using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InnerMsgQueue = System.Collections.Concurrent.ConcurrentQueue<CommonServerLib.InnerMsg>;

namespace CommonServerLib
{
    
    public class MsgToHostProgram
    {
        static InnerMsgQueue msgQueue = new InnerMsgQueue();
        
        static bool[] EnableInnerMsgTypeList = new bool[(int)LOG_STRING_TYPE.END];


        public static void EnableDisable(InnerMsgType type, bool enable)
        {
            EnableInnerMsgTypeList[(int)type] = enable;
        }

        public static bool IsEnable(InnerMsgType type)
        {
            return EnableInnerMsgTypeList[(int)type];
        }


        static public bool GetMsg(out InnerMsg msg)
        {
            return msgQueue.TryDequeue(out msg);
        }

        public static void ServerStart(int ServerID, int Port)
        {
            var msg = new InnerMsg() { Type = InnerMsgType.SERVER_START };
            msg.Value1 = string.Format("{0}_{1}", ServerID, Port);

            msgQueue.Enqueue(msg);
        }

        public static void CreateComponent()
        {
            var msg = new InnerMsg() { Type = InnerMsgType.CREATE_COMPONENT };
            msgQueue.Enqueue(msg);
        }

        public static void CurrentUserCount(int count)
        {
            if (IsEnable(InnerMsgType.CURRENT_CONNECT_COUNT) == false)
            {
                return;
            }

            var msg = new InnerMsg() { Type = InnerMsgType.CURRENT_CONNECT_COUNT };
            msg.Value1 = count.ToString();
            msgQueue.Enqueue(msg);
        }

        public static void UpdateLobbyInfo(int lobbyID, int lobbyUserCount)
        {
            if (IsEnable(InnerMsgType.CURRENT_LOBBY_INFO) == false)
            {
                return;
            }

            var msg = new InnerMsg() { Type = InnerMsgType.CURRENT_LOBBY_INFO };
            msg.Value1 = string.Format("{0}_{1}", lobbyID, lobbyUserCount);
            msgQueue.Enqueue(msg);
        }

        public static void UIUpdateInfo(string userCount, string lobbyUserCount)
        {
            var msg = new InnerMsg() { Type = InnerMsgType.UPDATE_UI_INFO };
            msg.Value1 = string.Format("{0}_{1}", userCount, lobbyUserCount);
            msgQueue.Enqueue(msg);
        }
    }


    public enum InnerMsgType
    {
        SERVER_START            = 0,
        CREATE_COMPONENT        ,
        CURRENT_CONNECT_COUNT   ,
        CURRENT_LOBBY_INFO      ,
        UPDATE_UI_INFO          ,

        END
    }

    public class InnerMsg
    {
        public InnerMsgType Type;
        public string SessionID;
        public string Value1;
        public string Value2;
    }
}
