using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using Moq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarsInvertoryServiceTests
{
    public class GetInvertoryViewModelAsyncTests : BaseCarsInvertoryServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetInvertoryViewModelAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutCars_ShouldReturnModelWithEmptyPropertiesCollection()
        {
            var dbContext = this.baseTest.GetDbContext();

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Empty(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCar()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Single(model.Cars);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithPrice()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Equal(2, model.Prices.Count);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithModelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Single(model.ModelTypes);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithSeries()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Equal(2, model.Series.Count);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithYear()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);

            var model = await this.CallGetInvertoryViewModelAsync(dbContext);

            Assert.Equal(2, model.Years.Count);
        }

        private async Task<CarsInvertoryViewModel> CallGetInvertoryViewModelAsync(ApplicationDbContext dbContext)
        {
            var service = this.GetService();
            var user = new Mock<ClaimsPrincipal>().Object;
            var model = await service.GetInvertoryViewModelAsync(dbContext.BaseCars, user, 1);

            return model;
        }
    }
}
