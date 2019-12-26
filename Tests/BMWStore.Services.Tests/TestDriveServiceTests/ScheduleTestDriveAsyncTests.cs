using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class ScheduleTestDriveAsyncTests : BaseTestDriveServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithNotSignInUser_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbCar.Id);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => 
                await this.CallScheduleTestDriveAsync(model, dbContext, null));
            Assert.Equal(ErrorConstants.NotSignIn, exception.Message);
        }

        [Fact]
        public async void WithModelWithIncorrectCarId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var carIncorrectId = Guid.NewGuid().ToString();
            var model = this.GetModel(carIncorrectId);

            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(async () => 
                await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithModel_ShouldScheduleTestDriveWithUpcomingStatus()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(TestDriveStatus.Upcoming.ToString(), dbContext.TestDrives.First().Status.Name);
        }

        [Fact]
        public async void WithAlreadyScheduledTestDrive_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            this.SheduleTestDrive(dbContext, dbUser.Id);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(TestDriveStatus.Upcoming.ToString(), dbContext.TestDrives.First().Status.Name);
        }

        [Fact]
        public async void WithScheduledTestDriveFromAnother_ShouldSheduleTestDrive()
        {
            var dbContext = this.GetDbContext();
            this.SheduleTestDrive(dbContext);
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(2, dbContext.TestDrives.Count());
        }

        private async Task CallScheduleTestDriveAsync(
            ScheduleTestDriveServiceModel model, 
            ApplicationDbContext dbContext,
            string userId)
        {
            SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var mockedUserManager = CommonGetMockMethods.GetUserManager();
			CommonSetupMockMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);
            var user = new Mock<ClaimsPrincipal>().Object;

            await service.ScheduleTestDriveAsync(model, user);
        }

        private ScheduleTestDriveServiceModel GetModel(string carId)
        {
            var model = new ScheduleTestDriveServiceModel()
            {
                CarId = carId,
                ScheduleDate = DateTime.UtcNow
            };

            return model;
        }

        private TestDrive SheduleTestDrive(ApplicationDbContext dbContext, string userId = "")
        {
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, userId, dbStatus);

            return dbTestDrive;
        }
    }
}
