﻿using BMWStore.Data;
using BMWStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Tests.Common.SeedTestMethods
{
    public static class SeedCarsMethods
    {
        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, string year = "") where TCar : BaseCar, new()
        {
            var dbCar = new TCar()
            {
                Year = year
            };
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

        public static TCar SeedCar<TCar>(ApplicationDbContext dbContext, params CarOption[] options)
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar()
            {
                Options = options.ToList()
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
            var dbEngine = SeedEnginesMethods.SeedEngineWithTransmission(dbContext);
            var dbFuelType = SeedFuelTypesMethods.SeedFuelType(dbContext);
            var dbSeries = SeedSeriesMethods.SeedSeries(dbContext);
            var dbModelType = SeedModelTypesMethods.SeedModelType(dbContext);
            dbCar.Name = Guid.NewGuid().ToString();
            dbCar.Pictures = pictures;
            dbCar.ModelType = dbModelType;
            dbCar.Series = dbSeries;
            dbCar.Engine = dbEngine;
            dbCar.FuelType = dbFuelType;
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
