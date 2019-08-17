using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class GetAllAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutTransmissions_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithTransmission_ShouldReturnTransmission()
        {
            var dbContext = this.GetDbContext();
            SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }

        [Fact]
        public async void WithTransmissionWithEngines_ShouldReturnCorrectEnginesCount()
        {
            var dbContext = this.GetDbContext();
            SeedEnginesMethods.SeedEngineWithTransmission(dbContext);
            SeedEnginesMethods.SeedEngine(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Equal(1, models.First().EnginesCount);
        }
    }
}
