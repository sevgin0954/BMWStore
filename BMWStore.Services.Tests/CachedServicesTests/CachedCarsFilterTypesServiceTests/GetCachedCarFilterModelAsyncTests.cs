using BMWStore.Common.Constants;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockTestMethods;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using Xunit;

namespace BMWStore.Services.Tests.CachedServicesTests.CachedCarsFilterTypesServiceTests
{
    public class GetCachedCarFilterModelAsyncTests : BaseCachedCarsFilterTypesServiceTest
    {
        [Fact]
        public async void WithNullKeyShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var mockedCache = new Mock<IDistributedCache>();
            var mockedCarsFilterTypesService = new Mock<ICarsFilterTypesService>();
            var service = this.GetService(mockedCache.Object, mockedCarsFilterTypesService.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.GetCachedCarFilterModelAsync(null, dbContext.BaseCars, dbContext.BaseCars));
            Assert.Equal(ErrorConstants.CantBeNullOrEmpty, exception.Message);
        }

        [Fact]
        public async void WithNotExistingKey_ShouldCallCarsFilterTypesService()
        {
            var dbContext = this.GetDbContext();
            var mockedCache = new Mock<IDistributedCache>();
            var key = Guid.NewGuid().ToString();
            CommonMockTestMethods.SetupMockedDistributedCacheGetAsync(mockedCache, key, null);
            var mockedCarsFilterTypesService = new Mock<ICarsFilterTypesService>();
            var service = this.GetService(mockedCache.Object, mockedCarsFilterTypesService.Object);

            await service.GetCachedCarFilterModelAsync(key, dbContext.BaseCars, dbContext.BaseCars);

            mockedCarsFilterTypesService
                .Verify(cfts => cfts.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars), Times.Once);
        }

        [Fact]
        public async void WithExistingKey_ShouldNotCallCarsFilterTypesService()
        {
            var dbContext = this.GetDbContext();
            var mockedCache = new Mock<IDistributedCache>();
            var key = Guid.NewGuid().ToString();
            var model = new CarsFilterViewModel();
            var mockedCarsFilterTypesService = new Mock<ICarsFilterTypesService>();
            CommonMockTestMethods.SetupMockedDistributedCacheGetAsync(mockedCache, key, model);
            var service = this.GetService(mockedCache.Object, mockedCarsFilterTypesService.Object);

            await service.GetCachedCarFilterModelAsync(key, dbContext.BaseCars, dbContext.BaseCars);

            mockedCarsFilterTypesService
                .Verify(cfts => cfts.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars), Times.Never);
        }
}
