using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        public void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalNewCarTestDrivesCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            
        }

        [Fact]
        public void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalUsedCarTestDrivesCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectNewCarsTestDrivesFromPast24HoursCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectUsedCarsTestDrivesFromPast24HoursCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectTestDrivesFromPast24HourCount()
        {

        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnModelWithCorrectTotalUsersCount()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var admin = this.SeedUser(WebConstants.AdminRoleName, dbContext);
            var user = this.SeedUser(WebConstants.UserRoleName, dbContext);

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
    }
}
