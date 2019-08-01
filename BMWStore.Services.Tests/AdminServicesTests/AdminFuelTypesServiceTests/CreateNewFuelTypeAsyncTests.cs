using BMWStore.Models.FuelTypeModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class CreateNewFuelTypeAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateNewFuelTypeAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewFuelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new AdminFuelTypeCreateBindingModel();
            await service.CreateNewFuelTypeAsync(model);

            Assert.Single(dbContext.FuelTypes);
        }
    }
}
