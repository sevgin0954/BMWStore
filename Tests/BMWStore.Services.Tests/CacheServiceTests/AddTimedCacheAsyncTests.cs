using BMWStore.Common.Constants;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.GetMethods;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public class AddTimedCacheAsyncTests : BaseCacheServiceTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void WithNullOrEmptyCacheKey_ShouldThrowException(string cacheKey)
        {
            var cache = new Mock<IDistributedCache>().Object;
            var service = this.GetService(cache);

            var obj = new object();
            var cacheType = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow;

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.AddTimedCacheAsync(obj, cacheKey, cacheType, expirationDate));
            var expectedExceptionMessage = ErrorConstants.CantBeNullOrEmptyParameter + "\r\nParameter name: cacheKey";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void WithNullOrEmptyCacheType_ShouldThrowException(string cacheType)
        {
            var cache = new Mock<IDistributedCache>().Object;
            var service = this.GetService(cache);

            var obj = new object();
            var cacheKey = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow;

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.AddTimedCacheAsync(obj, cacheKey, cacheType, expirationDate));
            var expectedExceptionMessage = ErrorConstants.CantBeNullOrEmptyParameter + "\r\nParameter name: cacheType";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async void WithPrevisionDateTime_ShouldThrowException()
        {
            var cache = new Mock<IDistributedCache>().Object;
            var service = this.GetService(cache);

            var obj = new object();
            var cacheKey = Guid.NewGuid().ToString();
            var cacheType = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow.AddSeconds(-1);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.AddTimedCacheAsync(obj, cacheKey, cacheType, expirationDate));
            Assert.Equal(ErrorConstants.InvalidDateTime, exception.Message);
        }

        [Fact]
        public async void WithCorrectParameters_ShouldCacheObj()
        {
            var cache = ServicesCreateMethods.CreateCache();
            var service = this.GetService(cache);

            var obj = Guid.NewGuid().ToString();
            var cacheKey = Guid.NewGuid().ToString();
            var cacheType = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow.AddDays(1);

            await service.AddTimedCacheAsync(obj, cacheKey, cacheType, expirationDate);


            Assert.True(cache.Get(cacheKey).Length > 0);
        }

        [Fact]
        public async void WithCorrectParameters_ShouldAddNewKeyToDictionary()
        {
            var cache = new Mock<IDistributedCache>().Object;
            var service = this.GetService(cache);

            var obj = Guid.NewGuid().ToString();
            var cacheKey = Guid.NewGuid().ToString();
            var cacheType = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow.AddDays(1);

            await service.AddTimedCacheAsync(obj, cacheKey, cacheType, expirationDate);

            var dictionary = this.GetDictionary(service);

            Assert.True(dictionary.ContainsKey(cacheType));
            Assert.Contains(dictionary[cacheType], value => value == cacheKey);
        }

        private IDictionary<string, HashSet<string>> GetDictionary(ICacheService service)
        {
            var fieldName = "cacheTypeCacheKeys";
            var dictionary = GetPrivateMethods.GetField(service, fieldName);

            return (IDictionary<string, HashSet<string>>)dictionary;
        }
    }
}
