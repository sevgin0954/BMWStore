using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
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
            var model = this.GetCarCreateModel();

            await service.CreateCarAsync<NewCar>(model);
            var dbCars = dbContext.BaseCars.ToList();

            Assert.Single(dbCars);
            Assert.IsType<NewCar>(dbCars.First());
        }

        [Fact]
        public async void WithTCarUsedCar_ShouldCreateUsedCar()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = this.GetCarCreateModel();

            await service.CreateCarAsync<UsedCar>(model);
            var dbCars = dbContext.BaseCars.ToList();

            Assert.Single(dbCars);
            Assert.IsType<UsedCar>(dbCars.First());
        }

        private AdminCarCreateBindingModel GetCarCreateModel()
        {
            var model = new AdminCarCreateBindingModel();

            return model;
        }
    }
}
