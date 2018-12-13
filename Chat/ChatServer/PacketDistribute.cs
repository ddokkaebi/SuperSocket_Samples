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
        ConnectSessionManager SessionManager = new ConnectSessionManager();
        PacketProcessor CommonPacketProcessor = null;
        List<PacketProcessor> PacketProcessorList = new List<PacketProcessor>();

        DBProcessor DBWorker = new DBProcessor();

        //LobbyManager LobbyMgr = new LobbyManager();
        RoomManager RoomMgr = new RoomManager();


        public ERROR_CODE Create(MainServer mainServer)
        {
            var roomThreadCount = ChatServerEnvironment.RoomThreadCount;
            //var lobbyCountPerThread = ChatServerEnvironment.LobbyCountPerThread;
            //var lobbyStartIndex = ChatServerEnvironment.LobbyStartNumber;
            //var maxLobbyUserCount = ChatServerEnvironment.MaxRoomCountPerLobby;

            Room.NetSendFunc = mainServer.SendData;

            SessionManager.CreateSession(mainServer.MaxConnectionNumber);

            //LobbyMgr.CreateLobby();
            RoomMgr.CreateRooms();

            CommonPacketProcessor = new PacketProcessor();
            CommonPacketProcessor.CreateAndStart(true, null, mainServer, SessionManager);
                        
            for (int i = 0; i < roomThreadCount; ++i)
            {
                //var lobbyBeginIndex = lobbyStartIndex + (i*lobbyCountPerThread);
                //var lobbyEndIndex = lobbyBeginIndex + lobbyCountPerThread;
                //var lobbyList = LobbyMgr.GetLobbyList(lobbyBeginIndex, lobbyEndIndex);

                var packetProcess = new PacketProcessor();
                packetProcess.CreateAndStart(false, RoomMgr.GetRoomList(i), mainServer, SessionManager);
                PacketProcessorList.Add(packetProcess);
            }

            var error = DBWorker.CreateAndStart(ChatServerEnvironment.DBWorkerThreadCount, DistributeDBJobResult, ChatServerEnvironment.RedisAddress);
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
            var packetId = (PACKETID)requestPacket.PacketID;
            var sessionIndex = requestPacket.SessionIndex;
                        
            if(IsClientRequestPacket(packetId) == false)
            {
                //TODO 로그 남기고, 어떤 처리를 해야 할듯
                return; 
            }

            if(IsClientRequestCommonPacket(packetId))
            {
                DistributeCommon(true, requestPacket);
                return;
            }


            var roomNumber = SessionManager.GetRoomNumber(sessionIndex);
            if(DistributeRoomProcessor(true, false, roomNumber, requestPacket) == false)
            {
                //TODO 로그 남기고, 어떤 처리를 해야 할듯      
            }            
        }

        public void DistributeCommon(bool isClientPacket, ServerPacketData requestPacket)
        {
            CommonPacketProcessor.InsertMsg(isClientPacket, requestPacket);
        }

        public bool DistributeRoomProcessor(bool isClientPacket, bool isPreRoomEnter, int roomNumber, ServerPacketData requestPacket)
        {
            var processor = PacketProcessorList.Find(x => x.관리중인_Room(roomNumber));
            if (processor != null)
            {
                if (isPreRoomEnter == false)
                {
                    //TODO 클라이언트 상태가 룸에 들어간 상태이어야 한다
                }

                processor.InsertMsg(isClientPacket, requestPacket);
                return true;
            }

            return false;
        }


        public void DistributeDBJobRequest(DBQueue dbQueue)
        {
            DBWorker.InsertMsg(dbQueue);
        }

        public void DistributeDBJobResult(DBResultQueue resultData)
        {
            var sessionIndex = resultData.SessionIndex;

            var requestPacket = new ServerPacketData();
            requestPacket.Assign(resultData);

            DistributeCommon(false, requestPacket);            
        }

        bool IsClientRequestCommonPacket(PACKETID packetId )
        {
            if ( packetId == PACKETID.REQ_LOGIN || packetId == PACKETID.REQ_ROOM_ENTER)
            {
                return true;
            }

            return false;
        }

        bool IsClientRequestPacket(PACKETID packetId)
        {
            return (PACKETID.CS_BEGIN < packetId && packetId < PACKETID.CS_END);
         }
    }
}
