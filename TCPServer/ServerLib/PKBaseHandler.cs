using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using CommonServerLib;

namespace ServerLib
{
    public class PKBaseHandler
    {
        protected ServerNetwork ServerNetworkRef;
        protected PacketDistributor PacketDistributorRef;
        protected UserManager UserManagerRef;
        protected LobbyManager LobbyManagerRef;


        public void Init(ServerNetwork serverNetwork, PacketDistributor packetDistributor, UserManager userManager, LobbyManager lobbyManager)
        {
            ServerNetworkRef = serverNetwork;
            PacketDistributorRef = packetDistributor;

            UserManagerRef = userManager;
            LobbyManagerRef = lobbyManager;
        }

        // 클라이언트에게 당장 접속을 끊어라고 요청한다.
        protected void NotifyDisConnectToClient(ERROR_CODE errorCode, string sessionID)
        {
            var packetData = new NTFDisConnect()
            {
                ErrorCode = (short)errorCode
            };

            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(packetData);
            //byte[] bodyData = Encoding.UTF8.GetBytes(jsonstring);
            //var sendData = PacketToBytes.Make(PACKETID.NTF_DIS_CONNECT, bodyData);

            //ServerNetworkRef.SendData(sessionID, sendData, PACKETID.NTF_DIS_CONNECT);
        }

        protected void RequestDBJob<T>(T RequestInfo, PACKETID packetID, string sessionID)
        {
            //string dbJobJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(RequestInfo);

            //var dbQueue = new DBQueue()
            //{
            //    PacketID = packetID,
            //    SessionID = sessionID,
            //    JsonFormatData = dbJobJsonString
            //};

            //PacketDistributorRef.RequestDBJob(dbQueue);
        }

        protected void LeaveLobby(LEAVE_LOBBY_TYPE leaveType, int lobbyID, string userID, ConnectUser user)
        {
            // user는 null 이라도 괜찮다!!!!!
            LobbyManagerRef.LeaveLobby(leaveType, lobbyID, userID, user);

            if (leaveType != LEAVE_LOBBY_TYPE.FAIL_REQUEST_ENTER)
            {
                var dbRequest = new DBReqRedisWriteString()
                {
                    UseRedisType = REDIS_TYPE.CHAT,
                    Key = DBJobWorkHandler.UserLobbyIDKey(userID),
                    Value = "0"
                };
                RequestDBJob<DBReqRedisWriteString>(dbRequest, PACKETID.REQ_EXECUTE_DB_SAVE_STRING_VALUE, "none");
            }


            var lobbyUserCount = LobbyManagerRef.CurrentUserCount(lobbyID);
            MsgToHostProgram.UpdateLobbyInfo(lobbyID, lobbyUserCount);
        }
    }
}
