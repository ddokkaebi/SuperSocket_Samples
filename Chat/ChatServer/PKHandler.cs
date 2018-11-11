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
        protected MainServer ServerNetwork;
        protected ConnectSessionManager SessionManager;


        public void Init(MainServer serverNetwork, ConnectSessionManager sessionManager)
        {
            ServerNetwork = serverNetwork;
            SessionManager = sessionManager;
        }

        public bool RequestDBJob(PacketDistributor distributor, DBQueue dbQueue)
        {
            distributor.RequestDBJob(dbQueue);
            return true;
        }

        public DBQueue MakeDBQueue(PACKETID packetID, string sessionID, string userID, byte[] jobDatas)
        {
            var dbQueue = new DBQueue()
            {
                PacketID    = packetID,
                SessionID   = sessionID,
                UserID      = userID, 
                Datas       = jobDatas
            };

            return dbQueue;
        }
    }
}
