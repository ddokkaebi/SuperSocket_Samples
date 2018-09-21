using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using CommonServerLib;

namespace ChatServer
{
    public class PKHandler
    {
        public bool RequestDBJob(PacketDistributor distributor, DBQueue dbQueue)
        {
            distributor.RequestDBJob(dbQueue);
            return true;
        }

        public DBQueue MakeDBQueue(PACKETID packetID, string sessionID, short lobbyID, string userID, byte[] jobDatas)
        {
            var dbQueue = new DBQueue()
            {
                PacketID    = packetID,
                SessionID   = sessionID,
                LobbyID     = lobbyID,
                UserID      = userID, 
                Datas       = jobDatas
            };

            return dbQueue;
        }
    }
}
