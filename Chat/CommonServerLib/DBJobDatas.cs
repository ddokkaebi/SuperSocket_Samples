﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessagePack;
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


    [MessagePackObject]
    public class DBReqLogin
    {
        [Key(0)]
        public string AuthToken;
    }

    [MessagePackObject]
    public class DBResLogin
    {
        [Key(0)]
        public string UserID;
        [Key(1)]
        public ERROR_CODE Result;
    }
}
