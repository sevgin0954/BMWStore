﻿using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public class GetCarsModelsAsyncTests : BaseCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetCarsModelsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithCar_ShouldReturnCorrectModel()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService();

            var model = await service.GetCarsModelsAsync<CarConciseViewModel>(dbContext.BaseCars, 1);

            Assert.Single(model);
            Assert.Equal(dbContext.BaseCars.First().Id, model.First().Id);
        }

        [Fact]
        public async void WithoutCars_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService();

            var model = await service.GetCarsModelsAsync<CarConciseViewModel>(dbContext.BaseCars, 1);

            Assert.Empty(model);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void WithCarAndSmallerThenOnePageNumber_ShouldReturnFirstPageModel(int pageNumber)
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService();

            var model = await service.GetCarsModelsAsync<CarConciseViewModel>(dbContext.BaseCars, pageNumber);

            Assert.Single(model);
        }

        [Fact]
        public async void WithCarsAndBiggerPageNumber_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            CommonSeedTestMethods.SeedCarWithEverything<NewCar>(dbContext);
            var service = this.GetService();

            var model = await service.GetCarsModelsAsync<CarConciseViewModel>(dbContext.BaseCars, 2);

            Assert.Empty(model);
        }
    }
}
