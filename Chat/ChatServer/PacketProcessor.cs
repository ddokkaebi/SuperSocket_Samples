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
        
        BufferBlock<ServerPacketData> MsgBuffer = new BufferBlock<ServerPacketData>();
        
        List<Lobby> LobbyList = null;
        Tuple<int,int> LobbyIndexRange = null;

        Dictionary<int, Action<ServerPacketData>> PacketHandlerMap = new Dictionary<int, Action<ServerPacketData>>();
        PKHCommon CommonPacketHandler = null;

        
        public void CreateAndStart(bool IsCommon, List<Lobby> lobbyList, MainServer mainServer, ConnectSessionManager sessionMgr)
        {
            공용_프로세서 = IsCommon;

            if (lobbyList != null)
            {
                LobbyList = lobbyList;
                LobbyIndexRange = new Tuple<int, int>(lobbyList[0].Index, (lobbyList[0].Index + LobbyList.Count()));
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

        public bool 관리중인_로비(int lobbyIndex)
        {
            return lobbyIndex.InRange(LobbyIndexRange.Item1, (LobbyIndexRange.Item2 - 1));
        }

        public void InsertMsg(bool isClientRequest, ServerPacketData data)
        {
            if (isClientRequest &&
                data.PacketID.InRange((int)PACKETID.CS_BEGIN, (int)PACKETID.CS_END) == false 
                )
            {
                return;
            }

            MsgBuffer.Post(data);
        }

        
        void RegistPacketHandler(MainServer serverNetwork, ConnectSessionManager sessionManager)
        {
            CommonPacketHandler = new PKHCommon();
            CommonPacketHandler.Init(serverNetwork, sessionManager);

            PacketHandlerMap.Add((int)PACKETID.NTF_IN_CONNECT_CLIENT, CommonPacketHandler.NotifyInConnectClient);
            PacketHandlerMap.Add((int)PACKETID.NTF_IN_DISCONNECT_CLIENT, CommonPacketHandler.NotifyInDisConnectClient);

            
            PacketHandlerMap.Add((int)PACKETID.REQ_LOGIN, CommonPacketHandler.RequestLogin);
            PacketHandlerMap.Add((int)PACKETID.RES_DB_LOGIN, CommonPacketHandler.ResponseLoginFromDB);
           
                        
            // 개발
            PacketHandlerMap.Add((int)PACKETID.REQ_TEST_ECHO, CommonPacketHandler.RequestTestEcho);
            //
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
