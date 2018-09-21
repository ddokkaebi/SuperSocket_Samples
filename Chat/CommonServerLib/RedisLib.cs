using CloudStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CommonServerLib
{
    public class RedisLib
    {
        RedisGroup redisGroup = null;


        public void Init(List<Tuple<string, int>> addressList)
        {
            var redisSettings = new RedisSettings[addressList.Count];

            for (int i = 0; i < addressList.Count; ++i)
            {
                redisSettings[i] = new RedisSettings($"{addressList[i].Item1}:{addressList[i].Item2}", db: 0);
            }

            redisGroup = new RedisGroup(groupName: "GameServer", settings: redisSettings);
        }

        public RedisList<string> GetRedisStringList(string key)
        {
            var redisObj = new RedisList<string>(redisGroup, key);
            return redisObj;
        }

        public RedisString<string> GetRedisString(string key)
        {
            var redisObj = new RedisString<string>(redisGroup, key);
            return redisObj;
        }   
        

        
    }
}
