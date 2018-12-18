using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks.Dataflow;

using CommonServerLib;
using CSBaseLib;

namespace ChatServer
{
    class PacketProcessor
    {
        bool 공용_프로세서 = false;

        bool IsThreadRunning = false;
        System.Threading.Thread ProcessThread = null;
        
        //TODO 동기 혹은 비동기로 주고 받을 때 receive쪽에서 처리하지 않으면 블럭킹 걸리나?
        //  그리고 기본으로 최대 버퍼 수는 몇개인지 확인하기
        BufferBlock<ServerPacketData> MsgBuffer = new BufferBlock<ServerPacketData>();

        //List<Lobby> LobbyList = null;
        Tuple<int,int> RoomNumberRange = new Tuple<int, int>(-1, -1);
        List<Room> RoomList = new List<Room>();

        Dictionary<int, Action<ServerPacketData>> PacketHandlerMap = new Dictionary<int, Action<ServerPacketData>>();
        PKHCommon CommonPacketHandler = new PKHCommon();
        PKHRoom RoomPacketHandler = new PKHRoom();
                

        //TODO MainServer를 인자로 주지말고, func을 인자로 넘겨주는 것이 좋다
        public void CreateAndStart(bool IsCommon, List<Room> roomList, MainServer mainServer, ConnectSessionManager sessionMgr)
        {
            공용_프로세서 = IsCommon;

            if (IsCommon == false)
            {
                RoomList = roomList;

                var minRoomNum = RoomList[0].Number;
                var maxRoomNum = RoomList[0].Number + RoomList.Count() - 1;
                RoomNumberRange = new Tuple<int, int>(minRoomNum, maxRoomNum);
            }

            RegistPacketHandler(mainServer, sessionMgr);

            IsThreadRunning = true;
            ProcessThread = new System.Threading.Thread(this.Process);
            ProcessThread.Start();
        }
        
        public void Destory()
        {
            IsThreadRunning = false;
            MsgBuffer.Complete();
        }

        public bool 관리중인_Room(int roomNumber)
        {
            //InRange의 min, max도 포함된다.
            return roomNumber.InRange(RoomNumberRange.Item1, RoomNumberRange.Item2);
        }

        public void InsertMsg(bool isClientRequest, ServerPacketData data)
        {
            if (isClientRequest && (data.PacketID.InRange((int)PACKETID.CS_BEGIN, (int)PACKETID.CS_END) == false))
            {
                return;
            }

            MsgBuffer.Post(data);
        }

        
        void RegistPacketHandler(MainServer serverNetwork, ConnectSessionManager sessionManager)
        {
            if (공용_프로세서)
            {
                CommonPacketHandler.Init(serverNetwork, sessionManager);
                CommonPacketHandler.SetConfig(ChatServerEnvironment.MaxUserCount);
                CommonPacketHandler.RegistPacketHandler(PacketHandlerMap);
                //PacketHandlerMap.Add((int)PACKETID.NTF_IN_CONNECT_CLIENT, CommonPacketHandler.NotifyInConnectClient);
                //PacketHandlerMap.Add((int)PACKETID.NTF_IN_DISCONNECT_CLIENT, CommonPacketHandler.NotifyInDisConnectClient);
                //PacketHandlerMap.Add((int)PACKETID.REQ_LOGIN, CommonPacketHandler.RequestLogin);
                //PacketHandlerMap.Add((int)PACKETID.RES_DB_LOGIN, CommonPacketHandler.ResponseLoginFromDB);
            }
            else
            {
                RoomPacketHandler.Init(RoomList);
                RoomPacketHandler.RegistPacketHandler(PacketHandlerMap);
            }

            // 개발
            PacketHandlerMap.Add((int)PACKETID.REQ_TEST_ECHO, CommonPacketHandler.RequestTestEcho);
        }

        void Process()
        {
            while (IsThreadRunning)
            {
                try
                {
                    var packet = MsgBuffer.Receive();

                    if (PacketHandlerMap.ContainsKey(packet.PacketID))
                    {
                        PacketHandlerMap[packet.PacketID](packet);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("세션 번호 {0}, PacketID {1}, 받은 데이터 크기: {2}", packet.SessionID, packet.PacketID, packet.BodyData.Length);
                    }
                }
                catch(Exception ex)
                {
                    IsThreadRunning.IfTrue(() => DevLog.Write(ex.ToString(), LOG_LEVEL.ERROR));
                }
            }
        }


    }
}
