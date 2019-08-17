using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Tests.Common.SeedTestMethods
{
    public static class SeedTestDrivesMethods
    {
        public static TestDrive SeedTestDrive(
            ApplicationDbContext dbContext, 
            string userId,
            Status status)
        {
            var dbTestDrive = new TestDrive()
            {
                UserId = userId,
                Status = status
            };

            SeedTestDrive(dbContext, dbTestDrive);

            return dbTestDrive;
        }

        public static TestDrive SeedTestDrive(ApplicationDbContext dbContext, BaseCar baseCar)
        {
            var dbTestDrive = new TestDrive()
            {
                Car = baseCar
            };

            SeedTestDrive(dbContext, dbTestDrive);

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithStatus(
            ApplicationDbContext dbContext,
            TestDriveStatus status = TestDriveStatus.Upcoming)
        {
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, status);
            var dbTestDrive = new TestDrive()
            {
                Status = dbStatus
            };
            SeedTestDrive(dbContext, dbTestDrive);

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithEverything(
            ApplicationDbContext dbContext,
            TestDriveStatus status = TestDriveStatus.Upcoming)
        {
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, status);
            var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);
            var dbTestDrive = new TestDrive()
            {
                Status = dbStatus,
                User = new User(),
                Car = dbCar
            };
            SeedTestDrive(dbContext, dbTestDrive);

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithCar<TCar>(
            ApplicationDbContext dbContext,
            string userId,
            Status status)
            where TCar : BaseCar, new()
        {
            var dbTestDrive = SeedTestDrive(dbContext, userId, status);
            var dbCar = SeedCarsMethods.SeedCarWithEverything<TCar>(dbContext);
            dbCar.TestDrives.Add(dbTestDrive);

            dbContext.SaveChanges();

            return dbTestDrive;
        }

        public static TestDrive SeedTestDriveWithCar<TCar>(
            ApplicationDbContext dbContext,
            DateTime dateTime,
            TestDriveStatus status = TestDriveStatus.Upcoming)
            where TCar : BaseCar, new()
        {
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, status);
            var dbTestDrive = new TestDrive()
            {
                ScheduleDate = dateTime,
                Status = dbStatus
            };
            var dbCar = SeedCarsMethods.SeedCar<TCar>(dbContext);
            dbCar.TestDrives.Add(dbTestDrive);

            SeedTestDrive(dbContext, dbTestDrive);

            return dbTestDrive;
        }

        private static void SeedTestDrive(ApplicationDbContext dbContext, TestDrive testDrive)
        {
            dbContext.TestDrives.Add(testDrive);
            dbContext.SaveChanges();
        }
    }
}
