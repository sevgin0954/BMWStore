using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.CarsFilterTypesServiceTests
{
    public class GetCarFilterModelAsyncTests : BaseCarsFilterTypesServiceTest
    {
        [Fact]
        public async void WithoutCars_ShouldReturnModelWithSingleDefaultFilterModels()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Single(model.Prices);
            Assert.Single(model.Series);
            Assert.Single(model.Years);
        }

        [Fact]
        public async void WithoutCars_ShouldReturnModelWithCorrectDefaultFilterModels()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Contains(model.Prices, p =>
                p.Text == WebConstants.AllFilterTypeModelText &&
                p.Value == WebConstants.AllFilterTypeModelValue);

            Assert.Contains(model.Series, s =>
                s.Text == WebConstants.AllFilterTypeModelText &&
                s.Value == WebConstants.AllFilterTypeModelValue);

            Assert.Contains(model.Years, y =>
                y.Text == WebConstants.AllFilterTypeModelText &&
                y.Value == WebConstants.AllFilterTypeModelValue);
        }

        [Fact]
        public async void WithAllCarsWithDifferentModelTypes_ShouldReturnModelWithCorrectModelTypes()
        {
            var dbContext = this.GetDbContext();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Equal(2, model.ModelTypes.Count);
            Assert.Contains(model.ModelTypes, mt => mt.Value == dbCar1.ModelType.Name && mt.Text == dbCar1.ModelType.Name + " (1)");
            Assert.Contains(model.ModelTypes, mt => mt.Value == dbCar2.ModelType.Name && mt.Text == dbCar2.ModelType.Name + " (1)");
        }

        [Fact]
        public async void WithFilteredCars_ShouldReturnModelWithCorrectSeries()
        {
            var dbContext = this.GetDbContext();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Equal(3, model.Series.Count);
            Assert.Contains(model.Series, mt => mt.Value == dbCar1.Series.Name && mt.Text == dbCar1.Series.Name + " (1)");
            Assert.Contains(model.Series, mt => mt.Value == dbCar2.Series.Name && mt.Text == dbCar2.Series.Name + " (1)");
        }

        [Fact]
        public async void WithFilteredCars_ShouldReturnModelWithCorrectYears()
        {
            var dbContext = this.GetDbContext();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            this.AddYearToCar(dbContext, dbCar1, "2020");
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            this.AddYearToCar(dbContext, dbCar2, "2019");
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Equal(3, model.Years.Count);
            Assert.Contains(model.Years, mt => mt.Value == dbCar1.Year && mt.Text == dbCar1.Year + " (1)");
            Assert.Contains(model.Years, mt => mt.Value == dbCar2.Year && mt.Text == dbCar2.Year + " (1)");
        }

        private void AddYearToCar(ApplicationDbContext dbContext, BaseCar car, string year)
        {
            car.Year = year;
            dbContext.SaveChanges();
        }
    }
}
