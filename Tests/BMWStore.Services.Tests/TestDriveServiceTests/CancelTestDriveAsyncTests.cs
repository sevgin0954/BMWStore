using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class CancelTestDriveAsyncTests : BaseTestDriveServiceTests
    {
        public Status CanceledStatus { get; private set; }

        [Fact]
        public async void WithIncorrectTestDriveId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var incorrectTestDriveId = Guid.NewGuid().ToString();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.CallCancelTestDriveAsync(dbContext, incorrectTestDriveId, dbUpcomingStatus.Id));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithUpcomingTestDriveFromAnotherUser_ShouldThrowExcpetion()
        {
            var dbContext = this.GetDbContext();
            var notSignInUser = SeedUsersMethods.SeedUser(dbContext);
            var signInUser = SeedUsersMethods.SeedUser(dbContext);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, notSignInUser.Id, dbUpcomingStatus);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.CallCancelTestDriveAsync(dbContext, dbTestDrive.Id, signInUser.Id));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCanceledTestDrive_ShouldThrowExcpetion()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, this.CanceledStatus);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.CallCancelTestDriveAsync(dbContext, dbTestDrive.Id, dbUser.Id));
            Assert.Equal(ErrorConstants.UpcomingStatusRequired, exception.Message);
        }

        [Fact]
        public async void WithPassedTestDrive_ShouldThrowExcpetion()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Passed);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbUpcomingStatus);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await this.CallCancelTestDriveAsync(dbContext, dbTestDrive.Id, dbUser.Id));
            Assert.Equal(ErrorConstants.UpcomingStatusRequired, exception.Message);
        }

        [Fact]
        public async void WithUpcomingTestDrive_ShouldCancelTestDrive()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbUpcomingStatus);

            await this.CallCancelTestDriveAsync(dbContext, dbTestDrive.Id, dbUser.Id);

            Assert.Equal(TestDriveStatus.Canceled.ToString(), dbTestDrive.Status.Name);
        }

        private async Task CallCancelTestDriveAsync(ApplicationDbContext dbContext, string testDriveId, string userId)
        {
            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, userId);
            await service.CancelTestDriveAsync(testDriveId, user);
        }

        private ITestDriveService GetService(ApplicationDbContext dbContext, string userId)
        {
            this.CanceledStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Canceled);
            var mockedUserManager = CommonMockMethods.GetMockedUserManager();
            CommonMockMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
