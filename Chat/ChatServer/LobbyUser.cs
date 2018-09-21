﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class LobbyUser
    {
        int IndexID;

        string SessionID;
        string UserID;

        public void Init(int indexID)
        {
            IndexID = indexID;
        }

        public void SetBasicInfo(string sessionID, string userID)
        {
            SessionID = sessionID;
            UserID = userID;
        }
    }
}