using BMWStore.Entities;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.CarModelTypeServiceTests
{
    public class GetModelTypeFilterModelsAsyncTests : BaseCarModelTypeServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetModelTypeFilterModelsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithouCars_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCorrectValue()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbCar = this.SeedCarWithModelType<UsedCar>(dbContext);

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Equal(dbCar.ModelTypeId, models.First().Value);
            Assert.Single(models);
        }

        [Fact]
        public async void WithNewCarsWithDifferentModels_ShouldReturnCorrectModelTypes()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbCar1 = this.SeedCarWithModelType<UsedCar>(dbContext);
            var dbCar2 = this.SeedCarWithModelType<UsedCar>(dbContext);

            var models = await service.GetModelTypeFilterModelsAsync(dbContext.BaseCars);

            Assert.Contains(models, m => m.Value == dbCar1.ModelTypeId);
            Assert.Contains(models, m => m.Value == dbCar2.ModelTypeId);
            Assert.Equal(2, models.Count);
        }
    }
}
