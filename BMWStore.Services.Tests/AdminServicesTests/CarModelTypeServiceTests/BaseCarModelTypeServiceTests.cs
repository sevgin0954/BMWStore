using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using System;

namespace BMWStore.Services.Tests.AdminServicesTests.CarModelTypeServiceTests
{
    public abstract class BaseCarModelTypeServiceTests
    {
        protected ICarModelTypeService GetService()
        {
            var service = new CarModelTypeService();

            return service;
        }

        protected TCar SeedCarWithModelType<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            this.AddModelTypeToCar(dbCar);
            dbContext.BaseCars.Add(dbCar);

            dbContext.SaveChanges();

            return dbCar;
        }

        private void AddModelTypeToCar(BaseCar baseCar)
        {
            var dbModelType = new ModelType()
            {
                Name = Guid.NewGuid().ToString()
            };
            baseCar.ModelType = dbModelType;
        }
    }
}
