using BMWStore.Services.CachedServices;
using BMWStore.Services.CachedServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace BMWStore.Services.Tests.CachedServicesTests.CachedCarsFilterTypesServiceTests
{
    public abstract class BaseCachedCarsFilterTypesServiceTest : BaseTest
    {
        public ICachedCarsFilterTypesService GetService(IDistributedCache cache, ICarsFilterTypesService carsFilterTypesService)
        {
            var cacheKeyService = new CacheKeysService();
            var service = new CachedCarsFilterTypesService(cache, carsFilterTypesService, cacheKeyService);

            return service;
        }
    }
}
