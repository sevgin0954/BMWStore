using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class GetCarsViewModelAsyncTests : BaseAdminCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetCarsViewModelAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutCars_ShouldReturnModelWithEmptyCarsCollection()
        {
            var dbContext = this.baseTest.GetDbContext();

            var model = await this.CallGetCarsViewModelAsync(dbContext);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithBiggerPage_ShouldReturnEmptyCarsCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetCarsViewModelAsync(dbContext, 2);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCar()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetCarsViewModelAsync(dbContext);

            Assert.Single(model.Cars);
        }

        private async Task<AdminCarsViewModel> CallGetCarsViewModelAsync(ApplicationDbContext dbContext, int page = 1)
        {
            var id = "";
            var service = this.GetService(dbContext);
            var model = await service.GetCarsViewModelAsync(
                id, 
                SortStrategyDirection.Ascending, 
                AdminBaseCarSortStrategyType.Condition, 
                AdminBaseCarFilterStrategy.All, page);

            return model;
        }
    }
}
