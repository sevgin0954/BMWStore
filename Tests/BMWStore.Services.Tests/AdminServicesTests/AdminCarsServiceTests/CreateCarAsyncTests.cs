using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.CreateMethods;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class CreateCarAsyncTests : BaseAdminCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithTCarNewCar_ShouldCreateNewCar()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new CarServiceModel();

            await service.CreateNewAsync<NewCar>(model);
            var dbCars = dbContext.NewCars.ToList();

            Assert.Single(dbCars);
        }

        [Fact]
        public async void WithTCarUsedCar_ShouldCreateUsedCar()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new CarServiceModel();

            await service.CreateNewAsync<UsedCar>(model);
            var dbCars = dbContext.UsedCars.ToList();

            Assert.Single(dbCars);
        }

        [Fact]
        public async void WithTCarUsedCarAndMileage_ShouldCreateCarWithMileage()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = CarServiceModelCreateMethods.Create("", 10);

            await service.CreateNewAsync<UsedCar>(model);
            var dbCar = dbContext.UsedCars.First();

            Assert.Equal(model.Mileage, dbCar.Mileage);
        }

        [Fact]
        public async void WithCarWithPicture_ShouldCreateCarWithPicture()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = CarServiceModelCreateMethods.Create("", Guid.NewGuid().ToString());

            await service.CreateNewAsync<UsedCar>(model);
            var dbCar = dbContext.UsedCars.First();

            Assert.Single(dbContext.Pictures);
            Assert.Equal(model.Pictures.First().PublicId, dbCar.Pictures.First().PublicId);
        }
    }
}