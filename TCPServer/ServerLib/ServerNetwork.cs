using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;

using CSBaseLib;
using CommonServerLib;

namespace ServerLib
{
    public class ServerNetwork : AppServer<ClientSession, EFBinaryRequestInfo>
    {
        public IBootstrap ActiveServerBootstrap;

        public Action<bool, ServerPacketData> SendToSystem = null;

         
         public ServerNetwork()
            : base(new DefaultReceiveFilterFactory<PacketReceiveFilter, EFBinaryRequestInfo>())
        {
            NewSessionConnected += new SessionHandler<ClientSession>(OnConnected);
            SessionClosed += new SessionHandler<ClientSession, CloseReason>(OnClosed);
            NewRequestReceived += new RequestHandler<ClientSession, EFBinaryRequestInfo>(OnPacketReceived);
        }

        public bool Create()
        {
            ActiveServerBootstrap = BootstrapFactory.CreateBootstrap();

            if (!ActiveServerBootstrap.Initialize())
            {
                DevLog.Write(string.Format("서버 초기화 실패"), LOG_LEVEL.ERROR);
                return false;
            }
            else
            {
                FileLogger.Write("<<<<<<<<<<< 서버 시작 >>>>>>>>>>>>", LOG_LEVEL.INFO);
                FileLogger.Write("서버 초기화 성공", LOG_LEVEL.INFO);
            }


            var result = ActiveServerBootstrap.Start();

            if (result != StartResult.Success)
            {
                DevLog.Write(string.Format("서버 시작 실패"), LOG_LEVEL.ERROR);
                return false;
            }
            else
            {
                FileLogger.Write("서버 시작 성공", LOG_LEVEL.INFO);
            }


            var appServer = ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;

            if (appServer.Config.MaxConnectionNumber < ServerEnvironment.MaxUserCount)
            {
                DevLog.Write(string.Format("서버 시작 실패. 서버 접속 가능 수가 채팅 유저 수보다 작다."), LOG_LEVEL.ERROR);
                return false;
            }
            
            return true;   
        }
                
        public void StopNetwork()
        {
            this.Stop();
        }
        
        // 꼭 네트워크 처리 스레드에서만 호출해야 한다. 세션을 저장한 스레드 이외에서 호출한다면 아래 구문을 호출해야 한다. 
        // var appServer = ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;
        // 인스턴스를 가져와야 한다.
        public bool SendData(string sessionID, byte[] sendData, PACKETID packetID )
        {
            var session = GetSessionByID(sessionID);

            if (session == null || session.EnableSend() == false)
            {
                return false;
            }

            try
            {
                session.Send(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                FileLogger.Write(string.Format("데이터 보내기 실패. session:{0}, PacketID:{1}, 에러:{2}", 
                                        sessionID, packetID, ex.ToString()), LOG_LEVEL.ERROR);

                session.SendFail();
                return false;
            }

            return true;
        }

        public void DisConnect(string sessionID, [CallerMemberName] string methodName = "")
        {
            try
            {
                ClientSession session = null;

                if (ActiveServerBootstrap == null) // SuperSocket 라이브러리에서 호출
                {
                    session = GetSessionByID(sessionID);
                }
                else   // 애플리케이션 측에서 호출
                {
                    var appServer = ActiveServerBootstrap.AppServers.FirstOrDefault() as ServerNetwork;
                    session = appServer.GetSessionByID(sessionID);
                }

                if (session != null)
                {
                    FileLogger.Write(string.Format("접속 종료. session:{0}, 호출한 곳:{1}",
                                            sessionID, methodName), LOG_LEVEL.INFO);

                    session.Close();
                }
                else
                {
                    FileLogger.Write(string.Format("접속 종료. 세션 없음. session:{0}, 호출한 곳:{1}",
                                            sessionID, methodName), LOG_LEVEL.DEBUG);
                }
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }
        

        void OnConnected(ClientSession session)
        {
            try
            {
                session.Clear();

                (DevLog.IsEnable(LOG_STRING_TYPE.CONNECTED)).IfTrue(() => DevLog.Write(string.Format("세션 번호 {0} 접속. {1}", session.SessionID, DateTime.Now), LOG_LEVEL.INFO));

                var packet = ServerPacketData.MakeNTFInConnectOrDisConnectClientPacket(true, session.SessionID);

                SendToSystem(false, packet);
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        void OnClosed(ClientSession session, CloseReason reason)
        {
            try
            {
                (DevLog.IsEnable(LOG_STRING_TYPE.DISCONNECTED)).IfTrue(() => DevLog.Write(string.Format("세션 번호 {0} 접속해제: {1}. {2}", session.SessionID, reason.ToString(), DateTime.Now), LOG_LEVEL.INFO));

                var packet = ServerPacketData.MakeNTFInConnectOrDisConnectClientPacket(false, session.SessionID);

                SendToSystem(false, packet);
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        void OnPacketReceived(ClientSession session, EFBinaryRequestInfo reqInfo)
        {
            try
            {
                (DevLog.IsEnable(LOG_STRING_TYPE.PACKET_RECEIVED)).IfTrue(() => DevLog.Write(string.Format("세션 번호 {0} 받은 데이터 크기: {1}, ThreadId: {2}", session.SessionID, reqInfo.Body.Length, System.Threading.Thread.CurrentThread.ManagedThreadId), LOG_LEVEL.TRACE));

                var packet = new ServerPacketData();
                packet.Assign(session.SessionID, reqInfo);

                SendToSystem(true, packet);
            }
            catch (Exception ex)
            {
                FileLogger.Write(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }
    }

    
}
