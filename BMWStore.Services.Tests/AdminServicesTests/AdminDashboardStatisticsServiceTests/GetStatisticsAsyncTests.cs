using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public class GetStatisticsAsyncTests : BaseAdminDashboardStatisticsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetStatisticsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutRecords_ShouldReturnModelWithPropertiesWithValuesZero()
        {
            var dbContext = this.baseTest.GetDbContext();
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
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedTestDrivesWithCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalNewCarsTestDrivesCount);
        }

        [Fact]
        public async void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalUsedCarTestDrivesCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedTestDrivesWithCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalUsedCarsTestDrivesCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectNewCarsTestDrivesFromPast24HoursCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            CommonCreateEntitiesTestMethods.CreateTestDrive(new NewCar(), dbContext, today);
            CommonCreateEntitiesTestMethods.CreateTestDrive(new NewCar(), dbContext, yeasterday);
            this.CreateTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.NewCarsTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectUsedCarsTestDrivesFromPast24HoursCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            this.CreateTestDrivesWithCars(dbContext, today);
            CommonCreateEntitiesTestMethods.CreateTestDrive(new UsedCar(), dbContext, yeasterday);
            this.CreateTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.UsedCarsTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnModelWithCorrectTestTotalDrivesFromPast24HourCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var yeasterday = this.GetYearterdayTime();
            var today = DateTime.UtcNow;
            this.CreateTestDrivesWithCars(dbContext, today);
            this.CreateTestDrivesWithCars(dbContext, yeasterday);
            this.CreateTestDrivesWithCars(dbContext, yeasterday);
            dbContext.SaveChanges();

            var model = await service.GetStatisticsAsync();

            Assert.Equal(2, model.TotalTestDrivesFromPast24HoursCount);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnModelWithCorrectTotalUsersCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            CommonSeedTestMethods.SeedUser(WebConstants.AdminRoleName, dbContext);
            CommonSeedTestMethods.SeedUser(WebConstants.UserRoleName, dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.TotalUsersCount);
        }

        [Fact]
        public async void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalNewCarsCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.NewCarsCount);
        }

        [Fact]
        public async void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalUsedCarsCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            this.SeedCars(dbContext);

            var model = await service.GetStatisticsAsync();

            Assert.Equal(1, model.UsedCarsCount);
        }

        private void SeedCars(ApplicationDbContext dbContext)
        {
            CommonCreateEntitiesTestMethods.CreateCar<NewCar>(dbContext);
            CommonCreateEntitiesTestMethods.CreateCar<UsedCar>(dbContext);

            dbContext.SaveChanges();
        }

        private void SeedTestDrivesWithCars(ApplicationDbContext dbContext)
        {
            var newCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            CommonCreateEntitiesTestMethods.CreateTestDrive(newCar, dbContext);
            var usedCar = CommonSeedTestMethods.SeedCar<UsedCar>(dbContext);
            CommonCreateEntitiesTestMethods.CreateTestDrive(usedCar, dbContext);

            dbContext.SaveChanges();
        }

        private void CreateTestDrivesWithCars(ApplicationDbContext dbContext, DateTime scheduleDate)
        {
            var newCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            CommonCreateEntitiesTestMethods.CreateTestDrive(newCar, dbContext, scheduleDate);
            var usedCar = CommonSeedTestMethods.SeedCar<UsedCar>(dbContext);
            CommonCreateEntitiesTestMethods.CreateTestDrive(usedCar, dbContext, scheduleDate);
        }

        private DateTime GetYearterdayTime()
        {
            var yeasterday = DateTime.UtcNow.AddDays(-1).AddMinutes(-1);

            return yeasterday;
        }
    }
}
