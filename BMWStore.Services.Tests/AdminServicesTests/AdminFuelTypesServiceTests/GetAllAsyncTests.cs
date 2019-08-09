using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutFuelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithFuelType_ShoudReturnFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedFuelTypesMethods.SeedFuelType(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}
