using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class GetAllAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutEngines_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithEngines_ShouldReturnEngines()
        {
            var dbContext = this.GetDbContext();
            SeedEnginesMethods.SeedEngineWithTransmission(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}
