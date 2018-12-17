using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

using CSBaseLib;
using InnerMessageHostProgram = CommonServerLib.MsgToHostProgram;
using LOG_LEVEL = CommonServerLib.LOG_LEVEL;

namespace ServerLib
{
    public class ServerLogic
    {
        ServerNetwork NetworkInstance = null;
        RemoteConnectCheck RemoteCheck = null;
        PacketDistributor Distributor = new PacketDistributor();
       

        public PacketDistributor GetPacketDistributor() { return Distributor; }

        public bool CreateNetwork()
        {
            CommonServerLib.ServerDefineData.Setting(ServerEnvironment.ChatServer.StartID, ServerEnvironment.ChatServer.LastID);

            NetworkInstance = new ServerNetwork();
            var result = NetworkInstance.Create();

            if (result == false)
            {
                DevLog.Write(string.Format("서버 네트워크 생성 실패"), LOG_LEVEL.ERROR);
                return false;
            }

            DevLog.Write(string.Format("서버 네트워크 생성 및 시작 성공"), LOG_LEVEL.INFO);
            return true;
        }

        public void StopServer()
        {
            if (RemoteCheck != null)
            {
                RemoteCheck.Stop();
            }

            NetworkInstance.StopNetwork();
            Distributor.Destory();
        }

        public ERROR_CODE CreateComponent()
        {
            var appServer = NetworkInstance.ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;
            
            InnerMessageHostProgram.ServerStart(ServerEnvironment.ChatServer.UniqueID, appServer.Config.Port);

            appServer.SendToSystem = Distributor.Distribute;

            var error = Distributor.Create(appServer);

            if (error != ERROR_CODE.NONE)
            {
                return error;
            }

            InnerMessageHostProgram.CreateComponent();

            WriteLogServerSettingInfo();

            return ERROR_CODE.NONE;
        }        

        

        // 어떤 종료의 유저 상태를 조사할지 설정한다.
        public void SetUserStatusCheckOption(UserStatusCheckOption option)
        {
            Distributor.SetUserStatusCheckOption(option);
        }

        public void StartRemoteConnectCheck(List<string> RemoteServers)
        {
            RemoteCheck = new RemoteConnectCheck();

            var remoteInfoList = new List<Tuple<string, string, int>>();

            foreach (var server in RemoteServers)
            {
                var infoList = server.Split(":");
                remoteInfoList.Add(new Tuple<string, string, int>(infoList[0], infoList[1], infoList[2].ToInt32()));

                DevLog.Write(string.Format("(To)연결할 서버 정보: {0}, {1}, {2}", infoList[0], infoList[1], infoList[2]), LOG_LEVEL.INFO);
            }

            RemoteCheck.Init(NetworkInstance.ActiveServerBootstrap, remoteInfoList);
        }

        void WriteLogServerSettingInfo()
        {
            var appServer = NetworkInstance.ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;

            FileLogger.Write(string.Format("Network Config - Ip: {0}, Port: {1}", appServer.Config.Ip, appServer.Config.Port), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - ListenBacklog: {0}", appServer.Config.ListenBacklog), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - KeepAliveInterval: {0}", appServer.Config.KeepAliveInterval), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - KeepAliveTime: {0}", appServer.Config.KeepAliveTime), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - ClearIdleSession: {0}", appServer.Config.ClearIdleSession.ToString()), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - ClearIdleSessionInterval: {0}", appServer.Config.ClearIdleSessionInterval), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - MaxConnectionNumber: {0}", appServer.Config.MaxConnectionNumber), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - MaxRequestLength: {0}", appServer.Config.MaxRequestLength), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - ReceiveBufferSize: {0}", appServer.Config.ReceiveBufferSize), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - SendBufferSize: {0}", appServer.Config.SendBufferSize), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - SendingQueueSize: {0}", appServer.Config.SendingQueueSize), LOG_LEVEL.INFO);
            FileLogger.Write(string.Format("Network Config - SendTimeOut: {0}", appServer.Config.SendTimeOut), LOG_LEVEL.INFO);

            FileLogger.Write(string.Format("모든 채팅 서버 ID {0} ~ {1}", ServerEnvironment.ChatServer.StartID, ServerEnvironment.ChatServer.LastID), LOG_LEVEL.INFO);

            FileLogger.Write(string.Format("채팅 서버의 UniqueName: {0}, UniqueID:{1}", ServerEnvironment.UniqueName, ServerEnvironment.ChatServer.UniqueID), LOG_LEVEL.INFO);

            FileLogger.Write(string.Format("채팅 서버의 최대 유저 수: {0}, 로비 시작 번호:{1}, 로비 최대 수:{2}", ServerEnvironment.MaxUserCount, ServerEnvironment.LobbyStartIndex, ServerEnvironment.LobbyCount), LOG_LEVEL.INFO);

            FileLogger.Write(string.Format("로비 당 최대 유저 수: {0}", ServerEnvironment.MaxUserPerLobby), LOG_LEVEL.INFO);

            FileLogger.Write(string.Format("DB 처리 스레드 수: {0}", ServerEnvironment.DBWorkerThreadCount), LOG_LEVEL.INFO);
            
            //WriteFileLog(string.Format("서버간 메시지를 읽어올 때 최대 개수(이것보다 많으면 다 삭제한다): {0}", ServerEnvironment.MaxS2SMessageReadCount), LOG_LEVEL.INFO);
            
            //WriteFileLog(string.Format("서버간 메시지를 읽어올 때 기준으로 {0}초 이전의 메시지는 버린다.", 120), LOG_LEVEL.INFO);
            
            //foreach (var redis in ServerEnvironment.GameRedisList)
            //{
            //    WriteFileLog(string.Format("Game RedisDB IP:{0}, Port:{1}", redis.Item1, redis.Item2), LOG_LEVEL.INFO);
            //}

            //foreach (var redis in ServerEnvironment.ChatRedisList)
            //{
            //    WriteFileLog(string.Format("Chat RedisDB IP:{0}, Port:{1}", redis.Item1, redis.Item2), LOG_LEVEL.INFO);
            //}
        }

        public void DisConnect(string sessionID, [CallerMemberName] string methodName = "")
        {
            NetworkInstance.DisConnect(sessionID, methodName);
        }



        public void Dev공지보내기(string message)
        {
            var appServer = NetworkInstance.ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;
            Distributor.Dev공지보내기(message);
        }

        public void Dev_SendMessageToChatServers(string server, string type, string sendMsg)
        {
            var appServer = NetworkInstance.ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;
            Distributor.Dev_SendMessageToChatServers(server, type, sendMsg);
        }

        public void Dev_SaveUserInfo(string id, CommonServerLib.RedisLib.MemoryDBUserInfo userData)
        {
            Distributor.Dev_SaveUserInfo(id, userData);
        }
    }

    public struct UserStatusCheckOption
    {
        public bool CheckHeartBeat;
        public bool CheckTimeLimitUserAuth;
        public bool CheckTimeLimitUserLobby;
        public bool CheckHeavyNetworkTrafficUser;
    }
}
