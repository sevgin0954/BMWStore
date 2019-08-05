using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Moq;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class GetAllTestDrivesAsyncTests : BaseTestDriveServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllTestDrivesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, dbUser.Id);

            var models = await service.GetAllTestDrivesAsync(user);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnOnlyUserTestDrives()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUser = CommonSeedTestMethods.SeedUser(dbContext);
            CommonSeedTestMethods.SeedCarWithTestDrive<NewCar>(dbContext, dbUser.Id);
            CommonSeedTestMethods.SeedCarWithTestDrive<NewCar>(dbContext, "");

            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, dbUser.Id);

            var models = await service.GetAllTestDrivesAsync(user);

            Assert.Equal(dbUser.TestDrives.First().Id, models.First().Id);
            Assert.Single(models);
        }

        private ITestDriveService GetService(
            ApplicationDbContext dbContext, 
            string userId)
        {
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
