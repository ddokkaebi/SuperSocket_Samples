using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;
using CSBaseLib;

namespace ChatServer
{
    public class PacketDistributor
    {
        PacketProcessor CommonPacketProcessor = null;
        List<PacketProcessor> PacketProcessorList = new List<PacketProcessor>();

        DBProcessor DBWorker = new DBProcessor();

        LobbyManager LobbyMgr = new LobbyManager();


        public ERROR_CODE Create(MainServer mainServer)
        {
            var lobbyThreadCount = ChatServerEnvironment.LobbyThreadCount;
            var lobbyCountPerThread = ChatServerEnvironment.LobbyCountPerThread;
            var lobbyStartIndex = ChatServerEnvironment.LobbyStartIndex;
            var maxLobbyUserCount = ChatServerEnvironment.MaxUserPerLobby;


            LobbyMgr.CreateLobby((lobbyThreadCount * lobbyCountPerThread), lobbyStartIndex, maxLobbyUserCount);
            
            CommonPacketProcessor = new PacketProcessor();
            CommonPacketProcessor.CreateAndStart(true, null, mainServer);
                        
            for (int i = 0; i < lobbyThreadCount; ++i)
            {
                var lobbyBeginIndex = lobbyStartIndex + (i*lobbyCountPerThread);
                var lobbyEndIndex = lobbyBeginIndex + lobbyCountPerThread;
                var lobbyList = LobbyMgr.GetLobbyList(lobbyBeginIndex, lobbyEndIndex);

                var packetProcess = new PacketProcessor();
                packetProcess.CreateAndStart(false, lobbyList, mainServer);
                PacketProcessorList.Add(packetProcess);
            }


            var error = DBWorker.CreateAndStart(ChatServerEnvironment.DBWorkerThreadCount, DBWorkResultFunc, ChatServerEnvironment.RedisAddress);
            if (error != ERROR_CODE.NONE)
            {
                return error;
            }

            return ERROR_CODE.NONE;
        }

        public void Destory()
        {
            DBWorker.Destory();

            CommonPacketProcessor.Destory();

            PacketProcessorList.ForEach(preocess => preocess.Destory());
            PacketProcessorList.Clear();
        }

        public void Distribute(ServerPacketData requestPacket)
        {
            var processor = PacketProcessorList.Find(x => x.관리중인_로비(requestPacket.Value1));

            if (processor != null)
            {
                processor.InsertMsg(true, requestPacket);
            }
            else
            {
                CommonPacketProcessor.InsertMsg(true, requestPacket);
            }
        }


        public void RequestDBJob(DBQueue dbQueue)
        {
            DBWorker.InsertMsg(dbQueue);
        }

        public void DBWorkResultFunc(DBResultQueue resultData)
        {
            var requestPacket = new ServerPacketData();
            requestPacket.Assign(resultData);

            var processor = PacketProcessorList.Find(x => x.관리중인_로비(requestPacket.Value1));

            if (processor != null)
            {
                processor.InsertMsg(false, requestPacket);
            }
            else
            {
                CommonPacketProcessor.InsertMsg(false, requestPacket);
            }
        }

    }
}
