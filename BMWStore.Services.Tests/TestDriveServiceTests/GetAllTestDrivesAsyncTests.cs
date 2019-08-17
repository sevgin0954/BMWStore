using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockTestMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class GetAllTestDrivesAsyncTests : BaseTestDriveServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, dbUser.Id);

            var models = await service.GetAllTestDrivesAsync(user);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithTestDrives_ShouldReturnOnlyUserTestDrives()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var upcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, dbUser.Id, upcomingStatus);
            SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", upcomingStatus);

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
