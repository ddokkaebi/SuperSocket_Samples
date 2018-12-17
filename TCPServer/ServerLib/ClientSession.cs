using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;

namespace ServerLib
{
    public class ClientSession : AppSession<ClientSession, CommonServerLib.EFBinaryRequestInfo>
    {
        // 보내기 실패 횟수
        int SendFailCount = 0; 


        public void Clear()
        {
            SendFailCount = 0;
        }
        
        public void SendFail() { ++SendFailCount; }
        
        public bool EnableSend()
        {
            return SendFailCount > 0 ? false : true;
        }
        

    }
}
