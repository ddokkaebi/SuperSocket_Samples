using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessagePack;

using CSBaseLib;

namespace CommonServerLib
{
    class DBJobWorkHandler
    {
        RedisLib RefRedis = null;

        public Tuple<ERROR_CODE,string> Init(RedisLib redis)
        {
            try
            {
                RefRedis = redis;

                // 미리 Redis와 연결이 되도록 여기서 더미 데이터를 요청한다.
                RefRedis.GetString("test");

                return new Tuple<ERROR_CODE,string>(ERROR_CODE.NONE, "");
            }
            catch(Exception ex)
            {
                return new Tuple<ERROR_CODE, string>(ERROR_CODE.REDIS_INIT_FAIL, ex.ToString());
            }
        }

        public DBResultQueue RequestLogin(DBQueue dbQueue)
        {
            try
            {
                var reqData = MessagePackSerializer.Deserialize<DBReqLogin>(dbQueue.Datas);

                // 필드 단위로 읽어 올 때는 꼭 Key가 있는지 확인 해야 한다!!!
                var redis = RefRedis.GetString(dbQueue.UserID);
                var value = redis.Result;                
                if (value.IsNullOrEmpty)
                {
                    return RequestLoginValue(ERROR_CODE.DB_LOGIN_EMPTY_USER, dbQueue);
                }


                //토큰 값##로그인시간
                var tokenString = value.ToString().Split("##");

                if( reqData.AuthToken != tokenString[0])
                {
                    return RequestLoginValue(ERROR_CODE.DB_LOGIN_INVALID_PASSWORD, dbQueue);
                }
                else
                {
                    return RequestLoginValue(ERROR_CODE.NONE, dbQueue);
                }
            }
            catch
            {
                return RequestLoginValue(ERROR_CODE.DB_LOGIN_EXCEPTION, dbQueue);
            }
        }

        DBResultQueue RequestLoginValue(ERROR_CODE result, DBQueue dbQueue)
        {
            var returnData = new DBResultQueue()
            {
                PacketID = PACKETID.RES_DB_LOGIN,
                SessionID = dbQueue.SessionID,
            };

            var resLoginData = new DBResLogin() { UserID = dbQueue.UserID, Result = result };
            returnData.Datas = MessagePackSerializer.Serialize(resLoginData);
            
            return returnData;
        }

    } // DBJobWorkHandler End
}
