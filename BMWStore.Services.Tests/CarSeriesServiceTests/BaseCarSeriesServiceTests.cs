using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using System;

namespace BMWStore.Services.Tests.CarSeriesServiceTests
{
    public abstract class BaseCarSeriesServiceTests
    {
        public ICarSeriesService GetService()
        {
            var service = new CarSeriesService();

            return service;
        }

        protected TCar SeedCarWithSeries<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            this.AddSeriesToCar(dbCar);
            dbContext.BaseCars.Add(dbCar);

            dbContext.SaveChanges();

            return dbCar;
        }

        private void AddSeriesToCar(BaseCar baseCar)
        {
            var dbSeries = new Series()
            {
                Name = Guid.NewGuid().ToString()
            };
            baseCar.Series = dbSeries;
        }
    }
}
