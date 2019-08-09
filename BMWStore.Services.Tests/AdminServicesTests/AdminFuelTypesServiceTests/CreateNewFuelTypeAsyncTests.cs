using BMWStore.Models.FuelTypeModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class CreateNewFuelTypeAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new AdminFuelTypeCreateBindingModel();
            await service.CreateNewFuelTypeAsync(model);

            Assert.Single(dbContext.FuelTypes);
        }
    }
}
