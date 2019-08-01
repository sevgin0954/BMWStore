using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutFuelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithFuelType_ShoudReturnFuelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedFuelType(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}
