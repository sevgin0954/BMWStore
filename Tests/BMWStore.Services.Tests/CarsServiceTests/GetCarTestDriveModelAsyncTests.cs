using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public class GetCarTestDriveModelAsyncTests : BaseCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutSignInUserAndCar_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public void WithoutSignInUserAndCar_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithTrueIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();
            this.ScheduleTestDrive(dbContext);

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled));
        }

        [Fact]
        public void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithCorrectTestDriveId()
        {
            var dbContext = this.GetDbContext();
            var dbTestDrive = this.ScheduleTestDrive(dbContext);

            var models = this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == dbTestDrive.Id));
        }

        private IEnumerable<CarServiceModel> CallGetCarsInventoryViewModelAsync(
            ApplicationDbContext dbContext,
            bool isUserSignIn,
            int pageNumber = 1)
        {
            var signInManager = this.GetSetupedSignInManager(isUserSignIn);
            var service = this.GetService(signInManager, dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;

            var models = service.GetCarTestDriveModel<CarServiceModel>(dbContext.BaseCars, user, pageNumber);

            return models;
        }

        private SignInManager<User> GetSetupedSignInManager(bool isSignIn)
        {
            var mockedSignInManager = CommonMockMethods.GetMockedSignInManager();
            CommonMockMethods.SetupMockedSignInManager(mockedSignInManager, isSignIn);

            return mockedSignInManager.Object;
        }

        private TestDrive ScheduleTestDrive(ApplicationDbContext dbContext)
        {
            var upcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(
                dbContext, 
                Guid.NewGuid().ToString(), 
                upcomingStatus);

            return dbTestDrive;
        }
    }
}
