using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.CarModelTypeServiceTests
{
    public class GetModelTypeFilterModelsAsyncTests : BaseCarModelTypeServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithouCars_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService();

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCorrectValue()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<UsedCar>(dbContext);

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Equal(dbCar.ModelType.Name, models.First().Value);
            Assert.Single(models);
        }

        [Fact]
        public async void WithCarsWithDifferentModels_ShouldReturnCorrectModelTypes()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<UsedCar>(dbContext);
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<UsedCar>(dbContext);

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Contains(models, m => m.Value == dbCar1.ModelType.Name);
            Assert.Contains(models, m => m.Value == dbCar2.ModelType.Name);
            Assert.Equal(2, models.Count);
        }
    }
}
