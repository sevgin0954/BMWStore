using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests
{
    public static class CommonCreateEntitiesTestMethods
    {
        public static Status CreateStatus(
            ApplicationDbContext dbContext, 
            TestDriveStatus testDriveStatus = TestDriveStatus.Upcoming)
        {
            var dbStatus = new Status()
            {
                Name = testDriveStatus.ToString()
            };
            dbContext.Add(dbStatus);

            return dbStatus;
        }

        public static TestDrive CreateTestDrive(BaseCar newCar, ApplicationDbContext dbContext, DateTime scheduleDate)
        {
            var testDrive = new TestDrive()
            {
                Car = newCar,
                ScheduleDate = scheduleDate
            };
            dbContext.TestDrives.Add(testDrive);

            return testDrive;
        }

        public static TestDrive CreateTestDrive(BaseCar newCar, ApplicationDbContext dbContext)
        {
            var testDrive = new TestDrive()
            {
                Car = newCar
            };
            dbContext.TestDrives.Add(testDrive);

            return testDrive;
        }

        public static TCar CreateCar<TCar>(ApplicationDbContext dbContext) 
            where TCar : BaseCar, new()
        {
            var dbCar = new TCar();
            dbContext.BaseCars.Add(dbCar);

            return dbCar;
        }
    }
}
