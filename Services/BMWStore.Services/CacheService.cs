using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
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

        public async Task AddTimedCacheAsync(object obj, string cacheKey, string cacheType, DateTime expirationDate)
        {
            this.ValidateCacheKey(cacheKey);
            this.ValidateCacheType(cacheType);
            DataValidator.ValidateMinDateTime(expirationDate, DateTime.UtcNow);

            var options = this.CreateOptions(expirationDate);
            var serielizedModelAsBytes = JSonHelper.Serialize(obj);
            await this.cache.SetAsync(cacheKey, serielizedModelAsBytes, options);

            this.AddKey(cacheType, cacheKey);
        }

        public async Task AddInfinityCacheAsync(object obj, string cacheKey, string cacheType)
        {
            this.ValidateCacheKey(cacheKey);
            this.ValidateCacheType(cacheType);

            var options = this.CreateOptions(DateTime.MaxValue);
            var serielizedModelAsBytes = JSonHelper.Serialize(obj);
            await this.cache.SetAsync(cacheKey, serielizedModelAsBytes, options);

            this.AddKey(cacheType, cacheKey);
        }

        private void AddKey(string cacheType, string key)
        {
            if (this.cacheTypeCacheKeys.ContainsKey(cacheType) == false)
            {
                this.cacheTypeCacheKeys[cacheType] = new HashSet<string>();
            }

            this.cacheTypeCacheKeys[cacheType].Add(key);
        }

        private DistributedCacheEntryOptions CreateOptions(DateTime dateTime)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = dateTime
            };

            return options;
        }

        public async Task<TResult> GetOrDefaultAsync<TResult>(string cacheKey) where TResult : class
        {
            this.ValidateCacheKey(cacheKey);

            var cachedResultAsBytes = await this.cache.GetAsync(cacheKey);
            if (cachedResultAsBytes == null)
            {
                return null;
            }

            var result = JSonHelper.Desirialize<TResult>(cachedResultAsBytes);

            return result;
        }

        private void ValidateCacheKey(string cacheKey)
        {
            var exception = new ArgumentException(ErrorConstants.CantBeNullOrEmptyParameter, nameof(cacheKey));
            DataValidator.ValidateNotNullOrEmpty(cacheKey, exception);
        }

        public async Task RemoveAsync(string cacheType)
        {
            this.ValidateCacheType(cacheType);

            if (this.cacheTypeCacheKeys.ContainsKey(cacheType))
            {
                var keys = this.cacheTypeCacheKeys[cacheType];
                foreach (var key in keys)
                {
                    await cache.RemoveAsync(key);
                }
            }
        }

        private void ValidateCacheType(string cacheType)
        {
            var exception = new ArgumentException(ErrorConstants.CantBeNullOrEmptyParameter, nameof(cacheType));
            DataValidator.ValidateNotNullOrEmpty(cacheType, exception);
        }
    }
}