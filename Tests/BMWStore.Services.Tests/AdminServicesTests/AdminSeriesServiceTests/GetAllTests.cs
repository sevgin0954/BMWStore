using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class GetAllTests : BaseAdminSeriesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutSeries_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Empty(models);
        }

        [Fact]
        public void WithSeries_ShouldReturnSeries()
        {
            var dbContext = this.GetDbContext();
            SeedSeriesMethods.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Single(models);
        }
    }
}