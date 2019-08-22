﻿using BMWStore.Services.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BMWStore.Services
{
    public class CacheKeysService : ICacheKeysService
    {
        private readonly IDictionary<string, HashSet<string>> cacheTypeCacheKeys;

        public CacheKeysService()
        {
            this.cacheTypeCacheKeys = new ConcurrentDictionary<string, HashSet<string>>();
        }

        public void AddKey(string cacheType, string key)
        {
            if (this.cacheTypeCacheKeys.ContainsKey(cacheType) == false)
            {
                this.cacheTypeCacheKeys[cacheType] = new HashSet<string>();
            }

            this.cacheTypeCacheKeys[cacheType].Add(key);
        }

        public IEnumerable<string> GetKeys(string cacheType)
        {
            var keys = new List<string>();

            if (this.cacheTypeCacheKeys.ContainsKey(cacheType))
            {
                foreach (var key in this.cacheTypeCacheKeys[cacheType])
                {
                    keys.Add(key);
                }
            }

            return keys;
        }
    }
}