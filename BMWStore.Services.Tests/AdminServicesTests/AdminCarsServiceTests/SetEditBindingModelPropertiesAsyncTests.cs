using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class SetEditBindingModelPropertiesAsyncTests: BaseAdminCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectModelId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(Guid.NewGuid().ToString());

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.SetEditBindingModelPropertiesAsync(model));
        }

        [Fact]
        public async void WithOption_ShouldSelectCarOptions()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddEnginesToAdminCarModel(model, dbContext.Engines);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.Contains(model.Engines, e => e.Value == dbCar.EngineId && e.Selected);
            Assert.Single(model.Engines, e => e.Selected);
        }

        [Fact]
        public async void WithoutOption_ShouldNotSelectCarOptions()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddEnginesToAdminCarModel(model, dbContext.Engines);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.DoesNotContain(model.Engines, e => e.Selected);
        }

        [Fact]
        public async void WithCarWithEngine_ShouldSelectCarEngine()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            SeedEnginesMethods.SeedEngine(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddEnginesToAdminCarModel(model, dbContext.Engines);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.Contains(model.Engines, e => e.Value == dbCar.EngineId && e.Selected);
            Assert.Single(model.Engines, e => e.Selected);
        }

        [Fact]
        public async void WithCarWithFuelType_ShouldSelectCarFuelType()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            SeedFuelTypesMethods.SeedFuelType(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddFuelTypesToAdminCarModel(model, dbContext.FuelTypes);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.Contains(model.FuelTypes, e => e.Value == dbCar.FuelTypeId && e.Selected);
            Assert.Single(model.FuelTypes, e => e.Selected);
        }

        [Fact]
        public async void WithCarWithModelType_ShouldSelectCarModelType()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            SeedModelTypesMethods.SeedModelType(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddModelTypesToAdminCarModel(model, dbContext.ModelTypes);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.Contains(model.ModelTypes, e => e.Value == dbCar.ModelTypeId && e.Selected);
            Assert.Single(model.ModelTypes, e => e.Selected);
        }

        [Fact]
        public async void WithCarWithSeries_ShouldSelectCarSeries()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            SeedSeriesMethods.SeedSeries(dbContext);
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(dbCar.Id);
            this.AddModelTypesToAdminCarModel(model, dbContext.ModelTypes);

            await service.SetEditBindingModelPropertiesAsync(model);

            Assert.Contains(model.ModelTypes, e => e.Value == dbCar.ModelTypeId && e.Selected);
            Assert.Single(model.ModelTypes, e => e.Selected);
        }

        private AdminCarEditBindingModel GetAdminCarModel(string id)
        {
            var model = new AdminCarEditBindingModel()
            {
                Id = id
            };

            return model;
        }

        private void AddEnginesToAdminCarModel(AdminCarEditBindingModel model, IEnumerable<Engine> engines)
        {
            model.Engines = engines
                .Select(e => new SelectListItem() { Value = e.Id })
                .ToList();
        }

        private void AddFuelTypesToAdminCarModel(AdminCarEditBindingModel model, IEnumerable<FuelType> fuelTypes)
        {
            model.FuelTypes = fuelTypes
                .Select(e => new SelectListItem() { Value = e.Id })
                .ToList();
        }

        private void AddModelTypesToAdminCarModel(AdminCarEditBindingModel model, IEnumerable<ModelType> modelTypes)
        {
            model.ModelTypes = modelTypes
                .Select(e => new SelectListItem() { Value = e.Id })
                .ToList();
        }
    }
}
