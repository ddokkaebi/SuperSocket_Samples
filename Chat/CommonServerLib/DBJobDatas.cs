using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace CommonServerLib
{
    public class DBQueue
    {
        public PACKETID PacketID;
        public string SessionID;
        public short LobbyID;
        public string UserID;
        public byte[] Datas;
    }

    public class DBResultQueue
    {
        public PACKETID PacketID;
        public string SessionID;
        public short LobbyID;
        public byte[] Datas;
    }


    public class DBReqLogin
    {
        public string AuthToken;
    }

    public class DBResLogin
    {
        public string UserID;
        public ERROR_CODE Result;
    }
}
