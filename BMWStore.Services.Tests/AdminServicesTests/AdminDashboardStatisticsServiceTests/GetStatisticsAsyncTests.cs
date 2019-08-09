using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public class GetStatisticsAsyncTests : BaseAdminDashboardStatisticsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutRecords_ShouldReturnModelWithPropertiesWithValuesZero()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(0, model.NewCarsCount);
            Assert.Equal(0, model.NewCarsTestDrivesFromPast24HoursCount);
            Assert.Equal(0, model.TotalNewCarsTestDrivesCount);
            Assert.Equal(0, model.TotalTestDrivesFromPast24HoursCount);
            Assert.Equal(0, model.TotalUsedCarsTestDrivesCount);
            Assert.Equal(0, model.TotalUsersCount);
            Assert.Equal(0, model.UsedCarsCount);
            Assert.Equal(0, model.UsedCarsTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalNewCarTestDrivesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedTestDrivesWithCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalNewCarsTestDrivesCount);
        }

        [Fact]
        public async void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalUsedCarTestDrivesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedTestDrivesWithCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalUsedCarsTestDrivesCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectNewCarsTestDrivesFromPast24HoursCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, today);
            SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, yeasterday);
            this.SeedTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.NewCarsTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectUsedCarsTestDrivesFromPast24HoursCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            this.SeedTestDrivesWithCars(dbContext, today);
            SeedTestDrivesMethods.SeedTestDriveWithCar<UsedCar>(dbContext, yeasterday);
            this.SeedTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.UsedCarsTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectTestTotalDrivesFromPast24HourCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            this.SeedTestDrivesWithCars(dbContext, today);
            this.SeedTestDrivesWithCars(dbContext, yeasterday);
            this.SeedTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(2, model.TotalTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnModelWithCorrectTotalUsersCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            SeedUsersMethods.SeedUser(dbContext, this.AdminRole);
            SeedUsersMethods.SeedUser(dbContext, this.UserRole);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalUsersCount);
        }

        [Fact]
        public async void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalNewCarsCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            SeedCarsMethods.SeedCar<NewCar>(dbContext);
            SeedCarsMethods.SeedCar<UsedCar>(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.NewCarsCount);
        }

        [Fact]
        public async void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalUsedCarsCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            SeedCarsMethods.SeedCar<NewCar>(dbContext);
            SeedCarsMethods.SeedCar<UsedCar>(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.UsedCarsCount);
        }

        private void SeedTestDrivesWithCars(ApplicationDbContext dbContext)
        {
            var newCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            SeedTestDrivesMethods.SeedTestDrive(dbContext, newCar);
            var usedCar = SeedCarsMethods.SeedCar<UsedCar>(dbContext);
            SeedTestDrivesMethods.SeedTestDrive(dbContext, usedCar);
        }

        private void SeedTestDrivesWithCars(ApplicationDbContext dbContext, DateTime scheduleDate)
        {
            SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, scheduleDate);
            SeedTestDrivesMethods.SeedTestDriveWithCar<UsedCar>(dbContext, scheduleDate);
        }

        private DateTime GetYearterdayTime()
        {
            var yeasterday = DateTime.UtcNow.AddDays(-1).AddMinutes(-1);

            return yeasterday;
        }
    }
}
