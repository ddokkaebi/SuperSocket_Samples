using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommonServerLib
{
    public class RedisLib
    {
        

        public void Init(List<Tuple<string, int>> gameDBList, List<Tuple<string, int>> chatDBList)
        {
            
        }

       

        public void AddStringList(REDIS_TYPE redisType, string key, string value, bool isAsync = true)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }
        public void AddStringObjectList<T>(REDIS_TYPE redisType, string key, T value, bool isAsync = true)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }


        public Int64 GetStringListCount(REDIS_TYPE redisType, string key)
        {
            try
            {
                return -1;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return 0;
            }
        }

        public string[] GetStringListAndAllDelete(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var valueGet = new RedisList<string>(GetRedisGroup(redisType), key).ToArray();
                //var valueAllDelete = new RedisList<string>(GetRedisGroup(redisType), key);
                //Task.WaitAll(valueGet, valueAllDelete.Clear());

                //return valueGet.Result;
                return null;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return null;
            }
        }
        public T[] GetStringObjectListAndAllDelete<T>(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var valueGet = new RedisList<T>(GetRedisGroup(redisType), key).ToArray();
                //var valueAllDelete = new RedisList<T>(GetRedisGroup(redisType), key);
                //Task.WaitAll(valueGet, valueAllDelete.Clear());

                //return valueGet.Result;
                return null;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return null;
            }
        }

        public void DeleteStringList(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var value = new RedisList<string>(GetRedisGroup(redisType), key);
                //value.Clear();
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        public bool SetObject<T>(REDIS_TYPE redisType, string key, T dataObject)
        {
            try
            {
                //var value = new CloudStructures.Redis.RedisString<T>(GetRedisGroup(redisType), key);
                //value.Set(dataObject);

                return true;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return false;
            }
        }

        public T GetObject<T>(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var value = new CloudStructures.Redis.RedisString<T>(GetRedisGroup(redisType), key).TryGet();
                //if (value.Result.Item1 == false)
                //{
                //return default(T);
                //}

                //return value.Result.Item2;
                return default(T);
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return default(T);
            }
        }

        public string GetString(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var value = new RedisString<string>(GetRedisGroup(redisType), key).TryGet();

                //if (value.Result.Item1 == false)
                //{
                //    return null;
                //}

                //return value.Result.Item2;
                return null;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return null;
            }
        }

        public void SetString(REDIS_TYPE redisType, string key, string value, bool isAsync = true)
        {
            try
            {
                //var redis = new CloudStructures.Redis.RedisString<string>(GetRedisGroup(redisType), key);

                //if (isAsync)
                //{
                //    var result = redis.Set(value);
                //}
                //else
                //{
                //    redis.Set(value).Wait();
                //}
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        public int GetInt(REDIS_TYPE redisType, string key)
        {
            try
            {
                //var value = new RedisString<int>(GetRedisGroup(redisType), key).TryGet();

                //if (value.Result.Item1 == false)
                //{
                //    return -1;
                //}

                //return value.Result.Item2;
                return -1;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return -1;
            }
        }

        public void SetInt(REDIS_TYPE redisType, string key, int value, bool isAsync = true)
        {
            try
            {
                //var redis = new CloudStructures.Redis.RedisString<int>(GetRedisGroup(redisType), key);

                //if (isAsync)
                //{
                //    var result = redis.Set(value);
                //}
                //else
                //{
                //    redis.Set(value).Wait();
                //}
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
            }
        }

        public MemoryDBUserInfo GetUserInfo(string id)
        {
            MemoryDBUserInfo userInfo = new MemoryDBUserInfo();

            try
            {
                //userInfo = GetObject<MemoryDBUserInfo>(REDIS_TYPE.GAME, id);
                return userInfo;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return userInfo;
            }
        }

        public MemoryDBGuildInfo GetUserNameGuildUnique(string id)
        {
            var userInfo = new MemoryDBGuildInfo();

            try
            {
                var key = id + "_G";
                userInfo = GetObject<MemoryDBGuildInfo>(REDIS_TYPE.GAME, key);
                return userInfo;
            }
            catch (Exception ex)
            {
                DBProcessor.WriteFileLog(ex.ToString(), LOG_LEVEL.ERROR);
                return userInfo;
            }
        }

        public void Dev_SaveUserInfo(string id, MemoryDBUserInfo userData)
        {
            SetObject<MemoryDBUserInfo>(REDIS_TYPE.GAME, id, userData);
        }


        public struct MemoryDBUserInfo
        {
            public Int64 UID { get; set; }           // UserIndex
            public string Token { get; set; }        // AuthToken
            public short CV { get; set; }            // ClientVersion
            public short CDV { get; set; }           // ClientDataVersion

        }

        public struct MemoryDBGuildInfo
        {
            public string Name { get; set; } // UserName
            public Int64 GU { get; set; }    // GuildUnique
        }
    }

    
}
