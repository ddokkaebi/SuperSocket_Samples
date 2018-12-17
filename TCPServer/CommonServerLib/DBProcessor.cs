using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks.Dataflow;
using System.Runtime.CompilerServices;

using CSBaseLib;
using FuncMap = System.Collections.Generic.Dictionary<CSBaseLib.PACKETID, System.Func<CommonServerLib.DBQueue, CommonServerLib.DBResultQueue>>;

namespace CommonServerLib
{
    public class DBProcessor
    {
        bool IsThreadRunning = false;
        List<System.Threading.Thread> ThreadList = new List<System.Threading.Thread>();

        BufferBlock<DBQueue> MsgBuffer = new BufferBlock<DBQueue>();

        FuncMap DBWorkHandlerMap = new FuncMap();

        DBJobWorkHandler DBWorkHandler = null;

        Action<DBResultQueue> DBWorkResultFunc = null;

        static Action<string, LOG_LEVEL, string, string, int> WriteLogToFileFunc = null;

        RedisLib RedisWraper = new RedisLib();


        public ERROR_CODE CreateAndStart(int threadCount, 
                                        Action<DBResultQueue> dbWorkResultFunc, 
                                        List<Tuple<string, int>> gameRedisList, List<Tuple<string, int>> chatRedisList)
        {
            //RedisWraper.Init(gameRedisList, chatRedisList);

            DBWorkResultFunc = dbWorkResultFunc;
            var error = RegistPacketHandler();

            if (error.Item1 != ERROR_CODE.NONE)
            {
                return error.Item1;
            }
            

            IsThreadRunning = true;

            for (int i = 0; i < threadCount; ++i)
            {
                var processThread = new System.Threading.Thread(this.Process);
                processThread.Start();

                ThreadList.Add(processThread);
            }

            return ERROR_CODE.NONE;
        }

        public void Destory()
        {
            IsThreadRunning = false;
            MsgBuffer.Complete();
        }

        public void InsertMsg(DBQueue dbQueue)
        {
            MsgBuffer.Post(dbQueue);
        }


        Tuple<ERROR_CODE, string> RegistPacketHandler()
        {
            DBWorkHandler = new DBJobWorkHandler();
            var error = DBWorkHandler.Init(RedisWraper);

            if (error.Item1 != ERROR_CODE.NONE)
            {
                return error;
            }


            //DBWorkHandlerMap.Add(PACKETID.REQ_DB_LOGIN, DBWorkHandler.RequestLogin);
            //DBWorkHandlerMap.Add(PACKETID.REQ_DB_ENTER_LOBBY, DBWorkHandler.RequestEnterLobby);
            //DBWorkHandlerMap.Add(PACKETID.REQ_DB_LEAVE_LOBBY, DBWorkHandler.RequestLeaveLobby);
            //DBWorkHandlerMap.Add(PACKETID.NTF_DB_OWN_DISCONNECT, DBWorkHandler.NotifyOwnDisconnect);
            //DBWorkHandlerMap.Add(PACKETID.NTF_DB_GUILD_CHAT, DBWorkHandler.NotifyGuildChat);
            //DBWorkHandlerMap.Add(PACKETID.REQ_DB_LOAD_USER_GUILD_INFO, DBWorkHandler.RequestLoadUserGuildInfo);

            //DBWorkHandlerMap.Add(PACKETID.REQ_EXECUTE_DB_SAVE_STRING_VALUE, DBWorkHandler.RequestExecDBSaveStringValue);
            //DBWorkHandlerMap.Add(PACKETID.REQ_DB_READ_S2S_MESSAGE, DBWorkHandler.RequestDBReadS2SMessage);

            return new Tuple<ERROR_CODE, string>(ERROR_CODE.NONE, "");
        }

        void Process()
        {
            while (IsThreadRunning)
            {
                try
                {
                    var dbJob = MsgBuffer.Receive();

                    if (DBWorkHandlerMap.ContainsKey(dbJob.PacketID))
                    {
                        var result = DBWorkHandlerMap[dbJob.PacketID](dbJob);
                        
                        (result.PacketID != PACKETID.INVALID).IfTrue(() => DBWorkResultFunc(result));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("세션 번호 {0}, DBWorkID {1}", dbJob.SessionID, dbJob.PacketID);
                    }
                }
                catch (Exception ex)
                {
                    IsThreadRunning.IfTrue(() => WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR));
                }
            }
        }

        
        public static void SetWriteFileLogFunction(Action<string, LOG_LEVEL, string, string, int> func)
        {
            WriteLogToFileFunc = func;
        }

        public static void WriteFileLog(string msg, LOG_LEVEL logLevel = LOG_LEVEL.TRACE,
                                [CallerFilePath] string fileName = "",
                                [CallerMemberName] string methodName = "",
                                [CallerLineNumber] int lineNumber = 0)
        {
            WriteLogToFileFunc(msg, logLevel, fileName, methodName, lineNumber);
        }




        public void Dev_SendMessageToChatServers(string server, string type, string sendMsg)
        {
            if (server == "CHAT")
            {
                string jsonstring = "";
                var tokens = sendMsg.Split("___");

                if (type == S2S_MESSAGE_TYPE.DIS_CONNECT.ToString())
                {
                    var s2sMsgData = new S2SMsgDisConnect()
                    {
                        UserID = tokens[0],
                        LobbyID = tokens[1].ToInt32(),
                    };

                    jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(s2sMsgData);
                }
                else if (type == S2S_MESSAGE_TYPE.GUILD_CHAT.ToString())
                {
                    var s2sMsgData = new S2SMsgGuildChat()
                    {
                        GU = tokens[0].ToInt64(),
                        Name = tokens[1],
                        Msg = tokens[2],
                    };

                    jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(s2sMsgData);
                }
                else
                {
                    return;
                }

                DBWorkHandler.SendMessageToChatServers(type, jsonstring);
            }
        }

        public void Dev_SaveUserInfo(string id, CommonServerLib.RedisLib.MemoryDBUserInfo userData)
        {
            RedisWraper.Dev_SaveUserInfo(id, userData);
        }


    }
}
