using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
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
            this.CreateTestDrivesWithCars(dbContext, today);
            this.CreateTestDrive(new NewCar(), dbContext, yeasterday);
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
            this.CreateTestDrive(new UsedCar(), dbContext, yeasterday);
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

            this.SeedUser(WebConstants.AdminRoleName, dbContext);
            this.SeedUser(WebConstants.UserRoleName, dbContext);

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

        private User SeedUser(string roleName, ApplicationDbContext dbContext)
        {
            var user = new User();
            var role = dbContext.Roles.Where(r => r.NormalizedName == roleName.ToUpper()).First();
            user.Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = role.Id
            });

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
        }

        private BaseCar CreateCar<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var car = new TCar();
            dbContext.BaseCars.Add(car);

            return car;
        }

        private void SeedCars(ApplicationDbContext dbContext)
        {
            this.CreateCar<NewCar>(dbContext);
            this.CreateCar<UsedCar>(dbContext);

            dbContext.SaveChanges();
        }

        private TestDrive CreateTestDrive(BaseCar newCar, ApplicationDbContext dbContext)
        {
            var testDrive = new TestDrive()
            {
                Car = newCar
            };

            dbContext.TestDrives.Add(testDrive);

            return testDrive;
        }

        private void SeedTestDrivesWithCars(ApplicationDbContext dbContext)
        {
            var newCar = this.CreateCar<NewCar>(dbContext);
            this.CreateTestDrive(newCar, dbContext);
            var usedCar = this.CreateCar<UsedCar>(dbContext);
            this.CreateTestDrive(usedCar, dbContext);

            dbContext.SaveChanges();
        }

        private void CreateTestDrivesWithCars(ApplicationDbContext dbContext, DateTime scheduleDate)
        {
            var newCar = this.CreateCar<NewCar>(dbContext);
            this.CreateTestDrive(newCar, dbContext, scheduleDate);
            var usedCar = this.CreateCar<UsedCar>(dbContext);
            this.CreateTestDrive(usedCar, dbContext, scheduleDate);
        }

        private TestDrive CreateTestDrive(BaseCar newCar, ApplicationDbContext dbContext, DateTime scheduleDate)
        {
            var testDrive = new TestDrive()
            {
                Car = newCar,
                ScheduleDate = scheduleDate
            };

            dbContext.TestDrives.Add(testDrive);

            return testDrive;
        }

        private DateTime GetYearterdayTime()
        {
            var yeasterday = DateTime.UtcNow.AddDays(-1).AddMinutes(-1);

            return yeasterday;
        }
    }
}
