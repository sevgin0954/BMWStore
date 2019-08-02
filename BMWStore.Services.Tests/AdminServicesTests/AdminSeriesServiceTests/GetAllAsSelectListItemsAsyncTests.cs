using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class GetAllAsSelectListItemsAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsSelectListItemsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutSeries_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var items
                = await service.GetAllAsSelectListItemsAsync();

            Assert.Empty(items);
        }

        [Fact]
        public async void WithSeries_ShouldReturnSeries()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            var items = await service.GetAllAsSelectListItemsAsync();

            Assert.Single(items);
        }
    }
}
