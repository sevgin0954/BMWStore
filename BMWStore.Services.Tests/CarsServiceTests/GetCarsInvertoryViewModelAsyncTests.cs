using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Tests.Common.MockTestMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public class GetCarsInventoryViewModelAsyncTests : BaseCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutSignInUserAndCar_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public async void WithoutSignInUserAndCar_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public async void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public async void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public async void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithTrueIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();
            this.ScheduleTestDrive(dbContext);

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled));
        }

        [Fact]
        public async void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithCorrectTestDriveId()
        {
            var dbContext = this.GetDbContext();
            var dbTestDrive = this.ScheduleTestDrive(dbContext);

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == dbTestDrive.Id));
        }

        private async Task<IEnumerable<CarInventoryConciseViewModel>> CallGetCarsInventoryViewModelAsync(
            ApplicationDbContext dbContext,
            bool isUserSignIn,
            int pageNumber = 1)
        {
            var signInManager = this.GetSetupedSignInManager(isUserSignIn);
            var service = this.GetService(signInManager, dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;

            var models = await service.GetCarScheduleViewModelAsync<CarInventoryConciseViewModel>(dbContext.BaseCars, user, pageNumber);

            return models;
        }

        private SignInManager<User> GetSetupedSignInManager(bool isSignIn)
        {
            var mockedSignInManager = CommonMockTestMethods.GetMockedSignInManager();
            CommonMockTestMethods.SetupMockedSignInManager(mockedSignInManager, isSignIn);

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
