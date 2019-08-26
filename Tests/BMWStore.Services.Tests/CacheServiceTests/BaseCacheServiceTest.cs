using BMWStore.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public abstract class BaseCacheServiceTest
    {
        public ICacheService GetService(IDistributedCache distributedCache)
        {
            var service = new CacheService(distributedCache);

            return service;
        }
    }
}
