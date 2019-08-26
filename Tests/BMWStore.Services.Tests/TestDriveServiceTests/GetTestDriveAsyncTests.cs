using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class GetTestDriveAsyncTests : BaseTestDriveServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithTestDriveOnAnotherUser_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, "", dbUpcomingStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetTestDriveAsync(dbTestDrive.Id, user));
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowExcception()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;
            var incorrectId = Guid.NewGuid().ToString();

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetTestDriveAsync(incorrectId, user));
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnTestDrive()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbUpcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id, dbUpcomingStatus);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            var model = await service.GetTestDriveAsync(dbTestDrive.Id, user);

            Assert.Equal(dbTestDrive.Id, model.Id);
        }

        private ITestDriveService GetService(ApplicationDbContext dbContext, string userId)
        {
            var mockedUserManager = CommonMockMethods.GetMockedUserManager();
            CommonMockMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
