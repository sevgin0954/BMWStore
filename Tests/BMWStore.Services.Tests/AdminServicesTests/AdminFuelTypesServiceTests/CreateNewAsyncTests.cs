using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class CreateNewAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new FuelTypeServiceModel();
            await service.CreateNewAsync(model);

            Assert.Single(dbContext.FuelTypes);
        }
    }
}
