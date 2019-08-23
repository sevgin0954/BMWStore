using BMWStore.Common.Constants;
using BMWStore.Helpers;
using BMWStore.Tests.Common.MockCreateMethods;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Text;
using Xunit;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public class GetOrDefaultAsyncTests : BaseCacheServiceTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void WithoutCacheKey_ShouldThrowExcpetion(string cacheKey)
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetOrDefaultAsync<object>(cacheKey));
            Assert.Equal(ErrorConstants.CantBeNullOrEmpty, exception.Message);
        }

        [Fact]
        public async void WithoutCache_ShouldReturnNull()
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);
            var cacheKey = Guid.NewGuid().ToString();

            var result = await service.GetOrDefaultAsync<object>(cacheKey);

            Assert.Null(result);
        }

        [Fact]
        public async void WithCache_ShouldReturnCache()
        {
            var cache = ServicesCreateMethods.CreateCache();
            var service = this.GetService(cache);
            var cacheKey = Guid.NewGuid().ToString();
            var str = Guid.NewGuid().ToString();
            var seriazedStr = this.GetSerializedTring(str);
            cache.SetString(cacheKey, seriazedStr);

            var result = await service.GetOrDefaultAsync<string>(cacheKey);

            Assert.Equal(str, result);
        }

        private string GetSerializedTring(string str)
        {
            var serializedStrAsBytes = JSonHelper.Serialize(str);
            var seriazedStr = Encoding.Default.GetString(serializedStrAsBytes);

            return seriazedStr;
        }
    }
}
