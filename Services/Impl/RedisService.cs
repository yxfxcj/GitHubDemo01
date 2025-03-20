using StackExchange.Redis;
using System.Text.Json;
using Ivan.DianPing.V2.Extensions;

namespace Ivan.DianPing.V2.Services.Impl
{
    public class RedisService
    {
        private readonly IDatabase _redisDb;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        #region Key操作

        public async Task SetKeyExAsync(string key, TimeSpan? expiry = null)
        {
            if (expiry == null) return;

            await _redisDb.KeyExpireAsync(key, expiry);
        }

        #endregion

        #region String操作

        public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _redisDb.StringSetAsync(key, value, expiry);
        }

        public async Task SetStringAsync(string key, object obj, TimeSpan? expiry = null)
        {

            var json = JsonSerializer.Serialize(obj);
            await _redisDb.StringSetAsync(key, json, expiry);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _redisDb.StringGetAsync(key);
        }

        public async Task<T> GetObjectFromStringAsync<T>(string key) where T : class
        {
            var json = await _redisDb.StringGetAsync(key);

            if (string.IsNullOrEmpty(json)) return default(T);

            return JsonSerializer.Deserialize<T>(json);
        }

        #endregion

        #region Hash操作

        public async Task HashSetObjectAsync<T>(string key, T entity, TimeSpan? expiry = null)
        {
            var hashFields = RedisObjectMapper.ToHashEntries(entity);
            await _redisDb.HashSetAsync(key, hashFields);
            await SetKeyExAsync(key, expiry);
        }

        public async Task<T?> HashGetObjectAsync<T>(string key) where T : class, new ()
        {
            HashEntry[] entries = await _redisDb.HashGetAllAsync(key);
            return entries.Length == 0
            ? default
            : RedisObjectMapper.FromHashEntries<T>(entries);
        }

        #endregion

    }
}
