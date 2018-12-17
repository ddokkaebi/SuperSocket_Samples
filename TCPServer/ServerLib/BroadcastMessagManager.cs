using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    // 전체 메시지 보내기 관리. 메시지를 로비 별로 나누어서 본내다.
    // 스레드 세이프 하지 않다.
    public class BroadcastMessagManager
    {
        int 한번에_보낼_로비_수;
        Queue<string> MessageQueue = new Queue<string>();
        int 현재까지_보낸_로비Index = 0;
        LobbyManager LobbyManagerRef;

        public void Init(LobbyManager lobbyManager, int 한번에_보낼_로비_수)
        {
            LobbyManagerRef = lobbyManager;
            this.한번에_보낼_로비_수 = 한번에_보낼_로비_수;
        }

        public void PushMessage(string message)
        {
            MessageQueue.Enqueue(message);
        }

        public void SendMessage()
        {
            if(MessageQueue.IsEmpty())
            {
                return;
            }

            var message = MessageQueue.Peek();
            var allLobbyCount = LobbyManagerRef.TotalLobbyCount();
            int realSendCount = 0;

            while (현재까지_보낸_로비Index < allLobbyCount)
            {
                var lobbyIndex = 현재까지_보낸_로비Index;

                if (LobbyManagerRef.ServerNotification(lobbyIndex, message))
                {
                    ++realSendCount;
                }

                if (realSendCount == 한번에_보낼_로비_수)
                {
                    break;
                }

                ++현재까지_보낸_로비Index;
            }

            if (현재까지_보낸_로비Index >= allLobbyCount)
            {
                현재까지_보낸_로비Index = 0;
                MessageQueue.Dequeue();
            }
        }

        public int MessageCount() { return MessageQueue.Count(); }

            
        
    }
}
