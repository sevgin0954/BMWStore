using BMWStore.Common.Constants;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace BMWStore.Services.Tests.CacheServiceTests
{
    public class RemoveAsync : BaseCacheServiceTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void WithoutCacheType_ShouldThrowExcpetion(string cacheType)
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetOrDefaultAsync<object>(cacheType));
            Assert.Equal(ErrorConstants.CantBeNullOrEmpty, exception.Message);
        }

        [Fact]
        public async void WithoutCache_ShouldDoNothing()
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);
            var cacheType = Guid.NewGuid().ToString();

            await service.RemoveAsync(cacheType);

            mockedCache.Verify(c => c.RemoveAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);

        }

        [Fact]
        public async void WithCache_ShouldRemoveCache()
        {
            var mockedCache = new Mock<IDistributedCache>();
            var service = this.GetService(mockedCache.Object);
            var cacheKey = Guid.NewGuid().ToString();
            var cacheType = Guid.NewGuid().ToString();
            await service.AddInfinityCacheAsync(new object(), cacheKey, cacheType);

            await service.RemoveAsync(cacheType);

            mockedCache.Verify(m => m.RemoveAsync(cacheKey, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
