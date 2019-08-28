using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using Xunit;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public class AddInfinityCacheAsyncTests : BaseCacheServiceTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void WithoutCacheKey_ShouldThrowExcpetion(string cacheKey)
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);
            var cacheType = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.AddInfinityCacheAsync(new object(), cacheKey, cacheType));
            var expectedExceptionMessage = ErrorConstants.CantBeNullOrEmptyParameter + "\r\nParameter name: cacheKey";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void WithoutCacheType_ShouldThrowException(string cacheType)
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);
            var cacheKey = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.AddInfinityCacheAsync(new object(), cacheKey, cacheType));
            var expectedExceptionMessage = ErrorConstants.CantBeNullOrEmptyParameter + "\r\nParameter name: cacheType";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async void WithCacheKeyAndType_ShouldCacheObj()
        {
            var cache = ServicesCreateMethods.CreateCache();
            var service = this.GetService(cache);
            var cacheKey = Guid.NewGuid().ToString();
            var cacheType = Guid.NewGuid().ToString();

            await service.AddInfinityCacheAsync(new object(), cacheKey, cacheType);

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

            await service.AddInfinityCacheAsync(obj, cacheKey, cacheType);

            var dictionary = this.GetDictionary(service);

            Assert.True(dictionary.ContainsKey(cacheType));
            Assert.Contains(dictionary[cacheType], value => value == cacheKey);
        }
    }
}
