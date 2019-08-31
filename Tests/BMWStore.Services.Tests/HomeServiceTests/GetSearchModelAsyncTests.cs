using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.HomeServiceTests
{
    public class GetSearchModelAsyncTests : BaseGetSearchModelAsyncTest
    {
        [Fact]
        public async void WithoutCars_ShouldReturnEmptyFilterModels()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetSearchModelAsync(dbContext.BaseCars, CarType.NewCar);

            Assert.Empty(model.CarTypes);
            Assert.Empty(model.ModelTypes);
            Assert.Empty(model.PriceRanges);
            Assert.Empty(model.Years);
        }

        [Fact]
        public async void WithCarsAndNewCarType_ShouldFilterYearsByNewCars()
        {
            var dbContext = this.GetDbContext();
            var newCarYear = "2020";
            var usedCarYear = "2018";
            SeedCarsMethods.SeedCar<NewCar>(dbContext, newCarYear);
            SeedCarsMethods.SeedCar<UsedCar>(dbContext, usedCarYear);
            var service = this.GetService(dbContext);

            var model = await service.GetSearchModelAsync(dbContext.BaseCars, CarType.NewCar);

            Assert.Single(model.Years);
            Assert.Single(model.Years, m => m.Value == newCarYear);
        }

        [Fact]
        public async void WithCarsAndNewCarType_ShouldFilterModelTypesByNewCars()
        {
            var dbContext = this.GetDbContext();
            var dbNewCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbUsedCar = SeedCarsMethods.SeedCarWithEverything<UsedCar>(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetSearchModelAsync(dbContext.BaseCars, CarType.NewCar);

            Assert.Single(model.ModelTypes);
            Assert.Single(model.ModelTypes, mt => mt.Value == dbNewCar.ModelType.Name);
        }
    }
}