using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;

namespace ChatServer
{
    public class ClientSession : AppSession<ClientSession, CommonServerLib.EFBinaryRequestInfo>
    {
        static ConcurrentBag<int> IndexPool = new ConcurrentBag<int>();
       
        public int SessionIndex { get; private set; } = -1;

        public static void CreateIndexPool(int maxCount)
        {
            for(int i = 0; i < maxCount; ++i)
            {
                IndexPool.Add(i);
            }
        }

        public static int PopIndex()
        {
            if (IndexPool.TryTake(out var result))
            {
                return result;
            }

            return -1;
        }

        public static void PushIndex(int index)
        {
            IndexPool.Add(index);
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();

            //TODO 세션 최대수를 supersocket 설정에서 정했는데 이것 이상으로 접속했을 때도 호출되는지 확인
            SessionIndex = PopIndex();
        }

        public override void Close()
        {
            base.Close();

            if (SessionIndex >= 0)
            {
                PushIndex(SessionIndex);
            }
        }
    }
}
