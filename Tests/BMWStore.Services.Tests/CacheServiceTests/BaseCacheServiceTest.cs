using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.GetMethods;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public abstract class BaseCacheServiceTest
    {
        protected ICacheService GetService(IDistributedCache distributedCache)
        {
            var service = new CacheService(distributedCache);

            return service;
        }

        protected IDictionary<string, HashSet<string>> GetDictionary(ICacheService service)
        {
            var fieldName = "cacheTypeCacheKeys";
            var dictionary = GetPrivateMethods.GetField(service, fieldName);

            return (IDictionary<string, HashSet<string>>)dictionary;
        }
    }
}
