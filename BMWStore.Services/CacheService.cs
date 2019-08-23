using BMWStore.Helpers;
using BMWStore.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDictionary<string, HashSet<string>> cacheTypeCacheKeys;
        private readonly IDistributedCache cache;

        public CacheService(IDistributedCache cache)
        {
            this.cacheTypeCacheKeys = new ConcurrentDictionary<string, HashSet<string>>();
            this.cache = cache;
        }

        public async Task AddInfinityCacheAsync(object obj, string cacheKey, string cacheType)
        {
            var serielizedModelAsBytes = JSonHelper.Serialize(obj);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.MaxValue
            };
            await this.cache.SetAsync(cacheKey, serielizedModelAsBytes, options);
            this.AddKey(cacheType, cacheKey);
        }

        public async Task<TResult> GetOrDefaultAsync<TResult>(string cacheKey) where TResult : class
        {
            var cachedResultAsBytes = await this.cache.GetAsync(cacheKey);
            if (cachedResultAsBytes == null)
            {
                return null;
            }

            var result = JSonHelper.Desirialize<TResult>(cachedResultAsBytes);

            return result;
        }

        public async Task RemoveAsync(string cacheType)
        {
            if (this.cacheTypeCacheKeys.ContainsKey(cacheType))
            {
                var keys = this.cacheTypeCacheKeys[cacheType];
                foreach (var key in keys)
                {
                    await cache.RemoveAsync(key);
                }
            }
        }

        private void AddKey(string cacheType, string key)
        {
            if (this.cacheTypeCacheKeys.ContainsKey(cacheType) == false)
            {
                this.cacheTypeCacheKeys[cacheType] = new HashSet<string>();
            }

            this.cacheTypeCacheKeys[cacheType].Add(key);
        }
    }
}