using BMWStore.Data;
using BMWStore.Entities;
using System;
using System.Collections.Generic;

namespace BMWStore.Tests.Common.SeedTestMethods
{
    public static class SeedCarsMethods
    {
        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            SeedCar(dbContext, dbCar);

            return dbCar;
        }

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, Picture picture)
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            dbCar.Pictures.Add(picture);
            SeedCar(dbContext, dbCar);

            return dbCar;
        }

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, ICollection<CarOption> options)
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar()
            {
                Options = options
            };
            SeedCar(dbContext, dbCar);

            return dbCar;
        }

        public static TCar SeedCarWithTestDrive<TCar>(
            ApplicationDbContext dbContext, 
            string userId,
            Status status)
            where TCar : BaseCar, new()
        {
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDrive(dbContext, userId, status);
            var dbCar = SeedCarWithEverything<TCar>(dbContext);
            dbCar.TestDrives.Add(dbTestDrive);

            dbContext.SaveChanges();

            return dbCar;
        }

        public static TCar SeedCarWithEverything<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var pictures = new List<Picture>() { new Picture() { PublicId = "" } };
            var dbCar = new TCar();
            dbCar.Name = Guid.NewGuid().ToString();
            dbCar.Pictures = pictures;
            dbCar.ModelType = new ModelType()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbCar.Series = new Series()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbCar.Engine = new Engine()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbCar.FuelType = new FuelType()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbCar.Options.Add(new CarOption() { Option = new Option() });

            SeedCar(dbContext, dbCar);

            return dbCar;
        }

        private static void SeedCar (ApplicationDbContext dbContext, BaseCar car)
        {
            dbContext.BaseCars.Add(car);
            dbContext.SaveChanges();
        }
    }
}
