using BMWStore.Data;
using BMWStore.Data.FilterStrategies.CarStrategies.CarMultipleStrategies;
using BMWStore.Entities;
using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarsInventoryServiceTests
{
    public class GetInventoryViewModelAsyncTests : BaseCarsInventoryServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutCars_ShouldReturnModelWithEmptyPropertiesCollection()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCar()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Single(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithPrice()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Single(model.Prices);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithModelType()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Single(model.ModelTypes);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithSeries()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Equal(2, model.Series.Count);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithYear()
        {
            var dbContext = this.GetDbContext();
            SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInventoryViewModelAsync(dbContext);

            Assert.Equal(2, model.Years.Count);
        }

        private async Task<CarsInventoryViewModel> CallGetInventoryViewModelAsync(ApplicationDbContext dbContext)
        {
            var service = this.GetService();
            var user = new Mock<ClaimsPrincipal>().Object;
            var filterStrategy = new ReturnAllMultipleFilterStrategy();
            var model = await service.GetInventoryViewModelAsync(filterStrategy, dbContext.BaseCars, user, 1);

            return model;
        }
    }
}
