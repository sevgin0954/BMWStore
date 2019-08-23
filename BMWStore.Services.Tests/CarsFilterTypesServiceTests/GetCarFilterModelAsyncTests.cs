using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.CarsFilterTypesServiceTests
{
    public class GetCarFilterModelAsyncTests : BaseCarsFilterTypesServiceTest
    {
        [Fact]
        public async void WithAllCarsWithDifferentModelTypes_ShouldReturnModelWithCorrectModelTypes()
        {
            var dbContext = this.GetDbContext();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Equal(2, model.ModelTypes.Count);
            Assert.Contains(model.ModelTypes, mt => mt.Value == dbCar1.ModelType.Name && mt.Text == dbCar1.ModelType.Name);
            Assert.Contains(model.ModelTypes, mt => mt.Value == dbCar2.ModelType.Name && mt.Text == dbCar2.ModelType.Name);
            Assert.True(model.ModelTypes.All(s => s.CarsCount == 1));
        }

        [Fact]
        public async void WithFilteredCars_ShouldReturnModelWithCorrectSeries()
        {
            var dbContext = this.GetDbContext();
            var dbCar1 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbCar2 = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetCarFilterModelAsync(dbContext.BaseCars, dbContext.BaseCars);

            Assert.Equal(2, model.Series.Count);
            Assert.Contains(model.Series, mt => mt.Value == dbCar1.Series.Name && mt.Text == dbCar1.Series.Name);
            Assert.Contains(model.Series, mt => mt.Value == dbCar2.Series.Name && mt.Text == dbCar2.Series.Name);
            Assert.True(model.Series.All(s => s.CarsCount == 1));
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

            Assert.Equal(2, model.Years.Count);
            Assert.Contains(model.Years, mt => mt.Value == dbCar1.Year && mt.Text == dbCar1.Year);
            Assert.Contains(model.Years, mt => mt.Value == dbCar2.Year && mt.Text == dbCar2.Year);
            Assert.True(model.Years.All(s => s.CarsCount == 1));
        }

        private void AddYearToCar(ApplicationDbContext dbContext, BaseCar car, string year)
        {
            car.Year = year;
            dbContext.SaveChanges();
        }
    }
}
