using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarTestDriveServiceTests
{
    public class GetCarTestDriveModelByIdTests : BaseCarTestDriveServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectCarId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var incorrectCarId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => 
					await this.CallGetCarViewModelAsync(dbContext, incorrectCarId, false, null));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithTestDrivesAndCarAndNotSignInUser_ShouldReturnIsTestDriveScheduledFalse()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", dbStatus);

            var model = await this.CallGetCarViewModelAsync(dbContext, dbCar.Id, false, null);

            Assert.False(model.IsTestDriveScheduled);
        }

        [Fact]
        public async void WithSignInUserWithTestDrive_ShouldReturnIsTestDriveScheduledTrue()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);
			var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, dbUser.Id, dbStatus);

            var model = await this.CallGetCarViewModelAsync(dbContext, dbCar.Id, true, dbUser.Id);

            Assert.True(model.IsTestDriveScheduled);
        }

		[Fact]
		public async void WithSignInUser_ShouldReturnCorrectIsTestDriveScheduledForTheCurrentUser()
		{
			var dbContext = this.GetDbContext();

			var dbSignInUser = SeedUsersMethods.SeedUser(dbContext);
			var dbNotSignInUser = SeedUsersMethods.SeedUser(dbContext);

			var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

			SeedTestDrivesMethods.SeedUpcomingTestDrive(dbContext, dbCar, dbNotSignInUser.Id);

			var model = await this.CallGetCarViewModelAsync(dbContext, dbCar.Id, true, dbSignInUser.Id);

			Assert.False(model.IsTestDriveScheduled);
		}

		[Fact]
		public async void WithUsersWithTestDrivesWithTheSameCar_ShouldReturnCorrectTestDriveIdForTheCurrentUser()
		{
			var dbContext = this.GetDbContext();

			var dbSignInUser = SeedUsersMethods.SeedUser(dbContext);
			var dbNotSignInUser = SeedUsersMethods.SeedUser(dbContext);

			var dbCar = SeedCarsMethods.SeedCarWithEverything<NewCar>(dbContext);

			var dbSignInUserTestDrive = SeedTestDrivesMethods.SeedUpcomingTestDrive(dbContext, dbCar, dbSignInUser.Id);
			var dbNotSignInUserTestDrive = SeedTestDrivesMethods.SeedUpcomingTestDrive(dbContext, dbCar, dbNotSignInUser.Id);

			var model = await this.CallGetCarViewModelAsync(dbContext, dbCar.Id, true, dbSignInUser.Id);

			Assert.Equal(dbSignInUserTestDrive.Id, model.TestDriveId);
		}

		[Fact]
        public async void WithSignInUserWithTestDrive_ShouldReturnCorrectTestDriveId()
        {
			var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);
			var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
			var dbCar = SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, dbUser.Id, dbStatus);
			

			var model = await this.CallGetCarViewModelAsync(dbContext, dbCar.Id, true, dbUser.Id);

			Assert.Equal(dbCar.TestDrives.First().Id, model.TestDriveId);
		}
		private async Task<CarServiceModel> CallGetCarViewModelAsync(
				 ApplicationDbContext dbContext,
				 string carId,
				 bool isUserSignIn,
				 string userId)
		{
			var signInManager = this.GetSetupedSignInManager(isUserSignIn);
			var userManager = this.GetSetupedUserManager(userId);
			var service = this.GetService(dbContext, signInManager, userManager);
			var user = new Mock<ClaimsPrincipal>().Object;
			var model = await service.GetCarTestDriveModelById<CarServiceModel>(carId, user);

			return model;
		}
	}
}
