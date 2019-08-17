using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data;
using BMWStore.Data.FilterStrategies.CarStrategies;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class GetCarsViewModelAsyncTests : BaseAdminCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutCars_ShouldReturnModelWithEmptyCarsCollection()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetCarsViewModelAsync(dbContext);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithBiggerPage_ShouldReturnEmptyCarsCollection()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetCarsViewModelAsync(dbContext, 2);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCar()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetCarsViewModelAsync(dbContext);

            Assert.Single(model.Cars);
        }

        private async Task<AdminCarsViewModel> CallGetCarsViewModelAsync(ApplicationDbContext dbContext, int page = 1)
        {
            var service = this.GetService(dbContext);
            var filterStrategy = new ReturnAllFilterStrategy();
            var model = await service.GetCarsViewModelAsync(
                filterStrategy,
                SortStrategyDirection.Ascending, 
                AdminBaseCarSortStrategyType.Condition, 
                page);

            return model;
        }
    }
}
