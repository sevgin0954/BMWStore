using BMWStore.Entities;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.SelectListItemsServiceTests
{
    public class GetAllAsSelectListItemsAsyncTests : BaseSelectListItemsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutElementInDB_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsSelectListItemsAsync<Engine>();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithEngine_ShouldReturnCarAsSelectListItem()
        {
            var dbContext = this.GetDbContext();
            var dbEngine = SeedEnginesMethods.SeedEngine(dbContext, Guid.NewGuid().ToString());
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsSelectListItemsAsync<Engine>();

            Assert.Single(models);
            Assert.Equal(dbEngine.Name, models.First().Text);
            Assert.Equal(dbEngine.Id, models.First().Value);
        }
    }
}
