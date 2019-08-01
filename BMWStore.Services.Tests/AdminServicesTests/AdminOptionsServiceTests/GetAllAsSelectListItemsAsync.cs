using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetAllAsSelectListItemsAsync : BaseAdminOptionsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsSelectListItemsAsync(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutOptions_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsSelectListItemsAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithOption_ShouldReturnOption()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedOption(dbContext);

            var models = await service.GetAllAsSelectListItemsAsync();

            Assert.Single(models);
        }
    }
}
