using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Moq;
using System;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class CancelTestDriveAsyncTests : BaseTestDriveServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CancelTestDriveAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithUpcomingTestDriveFromAnotherUser_ShouldThrowExcpetion()
        {
            var dbContext = this.baseTest.GetDbContext();
            var incorectUserId = Guid.NewGuid().ToString();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbUpcomingStatus = CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, incorectUserId, dbUpcomingStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.CancelTestDriveAsync(dbTestDrive.Id, user));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCanceledTestDrive_ShouldThrowExcpetion()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbCanceledStatus = CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Canceled);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbCanceledStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.CancelTestDriveAsync(dbTestDrive.Id, user));
            Assert.Equal(ErrorConstants.UpcomingStatusRequired, exception.Message);
        }

        [Fact]
        public async void WithPassedTestDrive_ShouldThrowExcpetion()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbUpcomingStatus = CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Passed);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbUpcomingStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.CancelTestDriveAsync(dbTestDrive.Id, user));
            Assert.Equal(ErrorConstants.UpcomingStatusRequired, exception.Message);
        }

        [Fact]
        public async void WithUpcomingTestDrive_ShouldCancelTestDrive()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbUpcomingStatus = CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbUpcomingStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            await service.CancelTestDriveAsync(dbTestDrive.Id, user);

            Assert.Equal(TestDriveStatus.Canceled.ToString(), dbTestDrive.Status.Name);
        }

        private ITestDriveService GetService(ApplicationDbContext dbContext, string userId)
        {
            CommonSeedTestMethods.SeedStatus(dbContext, TestDriveStatus.Canceled);
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
