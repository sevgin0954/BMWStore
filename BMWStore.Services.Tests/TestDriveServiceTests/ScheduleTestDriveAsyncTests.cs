using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.TestDriveModels.BindingModels;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class ScheduleTestDriveAsyncTests : BaseTestDriveServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public ScheduleTestDriveAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithNotSignInUser_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbContext, dbCar.Id);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => 
                await this.CallScheduleTestDriveAsync(model, dbContext, null));
            Assert.Equal(ErrorConstants.NotSignIn, exception.Message);
        }

        [Fact]
        public async void WithModelWithIncorrectCarId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var carIncorrectId = Guid.NewGuid().ToString();
            var model = this.GetModel(dbContext, carIncorrectId);

            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(async () => 
                await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithModel_ShouldScheduleTestDriveWithUpcomingStatus()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbContext, dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(TestDriveStatus.Upcoming.ToString(), dbContext.TestDrives.First().Status.Name);
        }

        [Fact]
        public async void WithAlreadyScheduledTestDrive_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            this.SheduleTestDrive(dbContext, dbUser.Id);
            var dbCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbContext, dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(TestDriveStatus.Upcoming.ToString(), dbContext.TestDrives.First().Status.Name);
        }

        [Fact]
        public async void WithScheduledTestDriveFromAnother_ShouldSheduleTestDrive()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SheduleTestDrive(dbContext);
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            var model = this.GetModel(dbContext, dbCar.Id);

            await this.CallScheduleTestDriveAsync(model, dbContext, dbUser.Id);

            Assert.Equal(2, dbContext.TestDrives.Count());
        }

        private async Task CallScheduleTestDriveAsync(
            ScheduleTestDriveBindingModel model, 
            ApplicationDbContext dbContext,
            string userId)
        {
            CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);
            var user = new Mock<ClaimsPrincipal>().Object;

            await service.ScheduleTestDriveAsync(model, user);
        }

        private ScheduleTestDriveBindingModel GetModel(ApplicationDbContext dbContext, string carId)
        {
            var model = new ScheduleTestDriveBindingModel()
            {
                CarId = carId,
                ScheduleDate = DateTime.UtcNow
            };

            return model;
        }

        private TestDrive SheduleTestDrive(ApplicationDbContext dbContext, string userId = "")
        {
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, userId, TestDriveStatus.Upcoming);

            return dbTestDrive;
        }
    }
}
