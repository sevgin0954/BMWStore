using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Tests.Common.MockTestMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public class GetCarViewModelAsyncTests : BaseCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectCarId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var incorrectCarId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await this.CallGetCarViewModelAsync(dbContext, incorrectCarId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithTestDrivesAndCarAndNotSignInUser_ShouldReturnIsTestDriveScheduledFalse()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", dbStatus);
            var mockedSignInManager = CommonMockTestMethods.GetMockedSignInManager();
            CommonMockTestMethods.SetupMockedSignInManager(mockedSignInManager, false);

            var model = await this.CallGetCarViewModelAsync(dbContext, mockedSignInManager.Object, dbCar.Id);

            Assert.False(model.IsTestDriveScheduled);
        }

        [Fact]
        public async void WithSignInUserWithTestDrive_ShouldReturnIsTestDriveScheduledTrue()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", dbStatus);
            var mockedSignInManager = CommonMockTestMethods.GetMockedSignInManager();
            CommonMockTestMethods.SetupMockedSignInManager(mockedSignInManager, true);

            var model = await this.CallGetCarViewModelAsync(dbContext, mockedSignInManager.Object, dbCar.Id);

            Assert.True(model.IsTestDriveScheduled);
        }

        [Fact]
        public async void WithSignInUserWithTestDrive_ShouldReturnCorrectTestDriveId()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", dbStatus);
            var mockedSignInManager = CommonMockTestMethods.GetMockedSignInManager();
            CommonMockTestMethods.SetupMockedSignInManager(mockedSignInManager, true);

            var model = await this.CallGetCarViewModelAsync(dbContext, mockedSignInManager.Object, dbCar.Id);

            Assert.Equal(dbCar.TestDrives.First().Id, model.TestDriveId);
        }

        private async Task<CarViewModel> CallGetCarViewModelAsync(
            ApplicationDbContext dbContext, 
            SignInManager<User> signInManager, 
            string carId)
        {
            var service = this.GetService(signInManager, dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;
            var model = await service.GetCarViewModelAsync(carId, user);

            return model;
        }

        private async Task<CarViewModel> CallGetCarViewModelAsync(ApplicationDbContext dbContext, string carId)
        {
            var service = this.GetService(dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;
            var model = await service.GetCarViewModelAsync(carId, user);

            return model;
        }
    }
}
