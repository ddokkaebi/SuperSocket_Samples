using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonServerLib;
using CSBaseLib;

namespace ServerLib
{
    // 패킷 처리를 위해 분배하는 클래스
    public class PacketDistributor
    {
        // 패킷 처리 클래스
        PacketProcessor LogicProcessor = null;

        // DB 작업 처리 클래스 
        DBProcessor DBWorker = new DBProcessor();


        public ERROR_CODE Create(ServerNetwork mainServer)
        {
            LogicProcessor = new PacketProcessor();
            LogicProcessor.CreateAndStart(mainServer, this);

            DBProcessor.SetWriteFileLogFunction(FileLogger.Write);

            var error = DBWorker.CreateAndStart(ServerEnvironment.DBWorkerThreadCount, 
                                                DBWorkResultFunc,
                                                ServerEnvironment.GameRedisList,   
                                                ServerEnvironment.ChatRedisList);
            if (error != ERROR_CODE.NONE)
            {
                return error;
            }

            SetSchedule();

            return ERROR_CODE.NONE;
        }

        public void Destory()
        {
            //ComLib.Scheduling.Scheduler.ShutDown();

            DBWorker.Destory();

            LogicProcessor.Destory();
        }

        // 패킷 처리를 요청한다. 스레드 세이프 하다.
        public void Distribute(bool isClientRequest, ServerPacketData requestPacket)
        {
            LogicProcessor.InsertMsg(isClientRequest, requestPacket);
        }

        // DB 작업을 요청한다. 스레드 세이프 하다.
        public void RequestDBJob(DBQueue dbQueue)
        {
            DBWorker.InsertMsg(dbQueue);
        }

        // DB 작업 완료 시 호출되는 함수.  스레드 세이프 하다.
        public void DBWorkResultFunc(DBResultQueue resultData)
        {
            var requestPacket = new ServerPacketData();
            requestPacket.Assign(resultData);

            LogicProcessor.InsertMsg(false, requestPacket);
        }

        // 주기적 작업을 등록한다.
        void SetSchedule()
        {
            // 다른 라이브러리로 대체한다
            // 가능하면 스케쥴러가 비슷한 시기에 동작하지 않도록 밀리세컨드로 설정한다.
            //ComLib.Scheduling.Scheduler.Schedule("LobbyInfoUpdate",
            //                        new ComLib.Scheduling.Trigger().Every(((int)5012).Milliseconds()),
            //                            () =>
            //                            {
            //                                SendSystemMessage(PACKETID.SYSTEM_LOBBY_INFO_UPDATE);
            //                            }
            //                        );

            //ComLib.Scheduling.Scheduler.Schedule("ServerCommunicationBus",
            //                        new ComLib.Scheduling.Trigger().Every(((int)64).Milliseconds()),
            //                            () =>
            //                            {
            //                                SendSystemMessage(PACKETID.SYSTEM_READ_SERVER_MESSAGE);
            //                            }
            //                        );

            //ComLib.Scheduling.Scheduler.Schedule("CheckUserStatus",
            //                        new ComLib.Scheduling.Trigger().Every(((int)455).Milliseconds()),
            //                            () =>
            //                            {
            //                                SendSystemMessage(PACKETID.SYSTEM_CHECK_USER_STATUS);
            //                            }
            //                        );

            //ComLib.Scheduling.Scheduler.Schedule("BroadcastMessag",
            //                        new ComLib.Scheduling.Trigger().Every(((int)62).Milliseconds()),
            //                            () =>
            //                            {
            //                                SendSystemMessage(PACKETID.SYSTEM_BROADCAST_MESSAG);
            //                            }
            //                        );

            //ComLib.Scheduling.Scheduler.Schedule("UIUpdate",
            //                        new ComLib.Scheduling.Trigger().Every(((int)2780).Milliseconds()),
            //                            () =>
            //                            {
            //                                SendSystemMessage(PACKETID.SYSTEM_UI_UPDATE);
            //                            }
            //                        );
        }
        
        // 시스템 메시지(내부용 메시지)를 보낸다.
        void SendSystemMessage(PACKETID packetID)
        {
            var packet = new ServerPacketData()
            {
                PacketID = (Int32)packetID
            };

            Distribute(false, packet);
        }

        // 어떤 종료의 유저 상태를 조사할지 설정한다.
        public void SetUserStatusCheckOption(UserStatusCheckOption option)
        {
            LogicProcessor.SetUserStatusCheckOption(option);
        }
       

        // 개발용
        public void Dev공지보내기(string message)
        {
            LogicProcessor.Dev공지보내기(message);
        }

        public void Dev_SendMessageToChatServers(string server, string type, string sendMsg)
        {
            DBWorker.Dev_SendMessageToChatServers(server, type, sendMsg);
        }

        public void Dev_SaveUserInfo(string id, CommonServerLib.RedisLib.MemoryDBUserInfo userData)
        {
            DBWorker.Dev_SaveUserInfo(id, userData);
        }
    }
}
