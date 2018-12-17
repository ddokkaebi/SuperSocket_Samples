using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;
using Newtonsoft.Json;

namespace CommonServerLib
{
    class MessageBusRedis
    {
        RedisLib RedisDBRef = null;
        int MaxGame2ChatMessageReadCount = 256;
        int MaxChat2ChatMessageReadCount = 1024;


        public Tuple<ERROR_CODE, string> Init(RedisLib redis, int maxS2SMessageReadCount)
        {
            try
            {
                MaxGame2ChatMessageReadCount = maxS2SMessageReadCount;

                RedisDBRef = redis;

                // 미리 Redis와 연결이 되도록 여기서 더미 데이터를 요청한다.
                RedisDBRef.GetString(REDIS_TYPE.GAME, "test");
                RedisDBRef.GetString(REDIS_TYPE.CHAT, "test");

                return new Tuple<ERROR_CODE, string>(ERROR_CODE.NONE, "");
            }
            catch (Exception ex)
            {
                return new Tuple<ERROR_CODE, string>(ERROR_CODE.REDIS_INIT_FAIL, ex.ToString());
            }
        }

        public DBResultQueue RequestDBReadS2SMessage(DBQueue dbQueue)
        {
            var reqData = JsonConvert.DeserializeObject<DBReqRedisWriteString>(dbQueue.JsonFormatData);

            var resulteValue = new DBResultQueue();

            var resS2SMessageData = new DBResReadS2SMessage();
            resS2SMessageData.MessageList = new List<S2SMessageData>();

            var curSecTime = Util.TimeTickToSec(DateTime.Now.Ticks);

            var gameToChatMsgCount = DBReadGameServer2ChatServerMessage(curSecTime, reqData.Key, ref resS2SMessageData);
            var chatToChatMsgCount = DBReadChatServer2ChatServerMessage(curSecTime, reqData.Key, ref resS2SMessageData);

            if (gameToChatMsgCount > 0 || chatToChatMsgCount > 0)
            {
                resulteValue.PacketID = PACKETID.RES_DB_READ_S2S_MESSAGE;
                resulteValue.JsonFormatData = Newtonsoft.Json.JsonConvert.SerializeObject(resS2SMessageData);
            }

            return resulteValue;
        }

        int DBReadGameServer2ChatServerMessage(Int64 curSecTime, string key, ref DBResReadS2SMessage resS2SMessageData)
        {
            try
            {
                var stopWatchWork = new System.Diagnostics.Stopwatch();

                // 현재 갯수가 너무 많으면 이것은 채팅서버가 죽은 상태에서 메시지가 막 쌓인 것으로 판단되어 메시지를 모두 날린다.
                var messageCountOfGame = RedisDBRef.GetStringListCount(REDIS_TYPE.GAME, key);
                if (messageCountOfGame < 1)
                {
                    return 0;
                }

                if (messageCountOfGame >= MaxGame2ChatMessageReadCount)
                {
                    RedisDBRef.DeleteStringList(REDIS_TYPE.GAME, key);
                    DBProcessor.WriteFileLog(string.Format("DBReadGameServer2ChatServerMessage. The message is too much: {0}", messageCountOfGame), LOG_LEVEL.ERROR);
                    return 0;

                }

                var errMsg1 = string.Format("DBReadGameServer2ChatServerMessage Read(Count:{0})", messageCountOfGame);
                StopWatchErrorWrite.중단_후_시간_오버이면_로그출력(stopWatchWork, 100, errMsg1);


                StopWatchErrorWrite.재시작(stopWatchWork);
                var valueList = RedisDBRef.GetStringListAndAllDelete(REDIS_TYPE.GAME, key);
                var errMsg2 = string.Format("DBReadGameServer2ChatServerMessage GetDelete(Count:{0})", messageCountOfGame);
                StopWatchErrorWrite.중단_후_시간_오버이면_로그출력(stopWatchWork, 100, errMsg2);

                foreach (var lowMessage in valueList)
                {
                    var tokens = lowMessage.Split("#$#");

                    if (tokens.Count() == 4) // 토큰이 최소 4개 이상은 되어야 한다.
                    {
                        Int64 timeSecond = 0;
                        if (Int64.TryParse(tokens[2], out timeSecond) == false)
                        {
                            DBProcessor.WriteFileLog(string.Format("DBReadGameServer2ChatServerMessage. Fail TimeSecond: {0}", tokens[2]), LOG_LEVEL.ERROR);
                            continue;
                        }

                        var diffSecond = curSecTime - timeSecond;
                        if (diffSecond >= 120)
                        {
                            DBProcessor.WriteFileLog(string.Format("DBReadGameServer2ChatServerMessage. Over Time DiffTimeSecond: {0}", diffSecond), LOG_LEVEL.ERROR);
                            continue;
                        }

                        var s2sMsg = new S2SMessageData
                        {
                            From = tokens[0],
                            Type = tokens[1],
                            Message = tokens[3]
                        };

                        resS2SMessageData.MessageList.Add(s2sMsg);
                    }
                    else
                    {
                        DBProcessor.WriteFileLog(string.Format("DBReadGameServer2ChatServerMessage. Fail Token: {0}", lowMessage), LOG_LEVEL.ERROR);
                    }
                }

                return resS2SMessageData.MessageList.Count();
            }
            catch
            {
                return 0;
            }
        }

        int DBReadChatServer2ChatServerMessage(Int64 curSecTime, string key, ref DBResReadS2SMessage resS2SMessageData)
        {
            try
            {
                // 현재 갯수가 너무 많으면 이것은 채팅서버가 죽은 상태에서 메시지가 막 쌓인 것으로 판단되어 메시지를 모두 날린다.
                var messageCountOfChat = RedisDBRef.GetStringListCount(REDIS_TYPE.CHAT, key);
                if (messageCountOfChat < 1)
                {
                    return 0;
                }

                if (messageCountOfChat >= MaxChat2ChatMessageReadCount)
                {
                    RedisDBRef.DeleteStringList(REDIS_TYPE.CHAT, key);
                    DBProcessor.WriteFileLog(string.Format("DBReadChatServer2ChatServerMessage. The message is too much: {0}", messageCountOfChat), LOG_LEVEL.ERROR);
                    return 0;
                }


                var valueList = RedisDBRef.GetStringObjectListAndAllDelete<ChatToChatMessage>(REDIS_TYPE.CHAT, key);

                foreach (var lowMessage in valueList)
                {
                    var diffSecond = curSecTime - lowMessage.ST;
                    if (diffSecond >= 120)
                    {
                        DBProcessor.WriteFileLog(string.Format("DBReadChatServer2ChatServerMessage. Over Time DiffTimeSecond: {0}", diffSecond), LOG_LEVEL.ERROR);
                        continue;
                    }

                    var s2sMsg = new S2SMessageData
                    {
                        From = "CHAT",
                        Type = lowMessage.Type,
                        Message = lowMessage.Msg,
                    };

                    resS2SMessageData.MessageList.Add(s2sMsg);
                }

                return resS2SMessageData.MessageList.Count();
            }
            catch
            {
                return 0;
            }
        }        
    }
}
