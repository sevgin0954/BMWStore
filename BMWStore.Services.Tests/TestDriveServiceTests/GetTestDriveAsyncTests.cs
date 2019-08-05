using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.Interfaces;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class GetTestDriveAsyncTests : BaseTestDriveServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetTestDriveAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithTestDriveOnAnotherUser_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, "");
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetTestDriveAsync(dbTestDrive.Id, user));
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowExcception()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;
            var incorrectId = Guid.NewGuid().ToString();

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetTestDriveAsync(incorrectId, user));
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnTestDrive()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, dbUser.Id);
            var service = this.GetService(dbContext, dbUser.Id);
            var user = new Mock<ClaimsPrincipal>().Object;

            var model = await service.GetTestDriveAsync(dbTestDrive.Id, user);

            Assert.Equal(dbTestDrive.Id, model.Id);
        }

        private ITestDriveService GetService(ApplicationDbContext dbContext, string userId)
        {
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
