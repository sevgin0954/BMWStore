using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetAllOptionsAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllOptionsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutOptions_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllOptionsAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithOption_ShouldReturnOption()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedOption(dbContext);

            var models = await service.GetAllOptionsAsync();

            Assert.Single(models);
        }
    }
}
