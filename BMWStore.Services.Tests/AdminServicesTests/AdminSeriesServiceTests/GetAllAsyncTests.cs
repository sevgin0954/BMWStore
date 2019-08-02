using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class GetAllAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutSeries_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithSeries_ShouldReturnSeries()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}
