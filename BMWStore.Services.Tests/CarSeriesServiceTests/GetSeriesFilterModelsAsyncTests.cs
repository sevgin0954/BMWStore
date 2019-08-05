using BMWStore.Entities;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.CarSeriesServiceTests
{
    public class GetSeriesFilterModelsAsyncTests : BaseCarSeriesServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetSeriesFilterModelsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithouCars_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService();

            var models = await service.GetSeriesFilterModelsAsync(dbContext.BaseCars);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithCar_ShouldReturnModelWithCorrectValue()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService();
            var dbCar = this.SeedCarWithSeries<UsedCar>(dbContext);

            var models = await service.GetSeriesFilterModelsAsync(dbContext.BaseCars);

            Assert.Equal(dbCar.SeriesId, models.First().Value);
            Assert.Single(models);
        }

        [Fact]
        public async void WithCarsWithDifferentModels_ShouldReturnCorrectModelTypes()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService();
            var dbCar1 = this.SeedCarWithSeries<UsedCar>(dbContext);
            var dbCar2 = this.SeedCarWithSeries<UsedCar>(dbContext);

            var models = await service.GetSeriesFilterModelsAsync(dbContext.BaseCars);

            Assert.Contains(models, m => m.Value == dbCar1.SeriesId);
            Assert.Contains(models, m => m.Value == dbCar2.SeriesId);
            Assert.Equal(2, models.Count);
        }
    }
}
