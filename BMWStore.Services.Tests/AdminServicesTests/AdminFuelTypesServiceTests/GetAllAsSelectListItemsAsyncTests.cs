using BMWStore.Data;
using BMWStore.Entities;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class GetAllAsSelectListItemsAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsSelectListItemsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutFuelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var items = await service.GetAllAsSelectListItemsAsync();

            Assert.Empty(items);
        }

        [Fact]
        public async void WithFuelType_ShoudReturnFuelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedFuelType(dbContext);

            var items = await service.GetAllAsSelectListItemsAsync();

            Assert.Single(items);
        }
    }
}
