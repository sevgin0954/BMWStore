using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.CarYearServiceTests
{
    public class GetYearFilterModelsAsyncTests : BaseCarYearServiceTest
    {
        [Fact]
        public async void WithouthCars_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService();
            
            var models = await service.GetYearFilterModelsAsync(dbContext.BaseCars);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithCarsInTheSameYear_ShouldReturnSingleCorrectFilterModel()
        {
            const string year = "2019";

            var dbContext = this.GetDbContext();
            this.SeedCarWithYear(dbContext, year);
            var service = this.GetService();

            var models = await service.GetYearFilterModelsAsync(dbContext.BaseCars);

            Assert.Single(models);
            Assert.Contains(models, m => m.Text == $"{year} ({1})");
        }

        [Fact]
        public async void WithCarsInDiferentYears_ShouldReturnTwoCorrectFilterModels()
        {
            const string year1 = "2019";
            const string year2 = "2018";

            var dbContext = this.GetDbContext();
            this.SeedCarWithYear(dbContext, year1);
            this.SeedCarWithYear(dbContext, year2);
            var service = this.GetService();

            var models = await service.GetYearFilterModelsAsync(dbContext.BaseCars);

            Assert.Equal(2, models.Count);
            Assert.Contains(models, m => m.Text == $"{year1} ({1})");
            Assert.Contains(models, m => m.Text == $"{year2} ({1})");
        }

        private void SeedCarWithYear(ApplicationDbContext dbContext, string year)
        {
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            dbCar.Year = year;

            dbContext.SaveChanges();
        }
    }
}
