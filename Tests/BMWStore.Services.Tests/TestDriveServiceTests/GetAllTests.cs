using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BMWStore.Services.Tests.TestDriveServiceTests
{
    public class GetAllTests : BaseTestDriveServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutTestDrives_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, dbUser.Id);

            var models = service.GetAll(user);

            Assert.Empty(models);
        }

        [Fact]
        public void WithTestDrives_ShouldReturnOnlyUserTestDrives()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var upcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, dbUser.Id, upcomingStatus);
            SeedCarsMethods.SeedCarWithTestDrive<NewCar>(dbContext, "", upcomingStatus);

            var user = new Mock<ClaimsPrincipal>().Object;
            var service = this.GetService(dbContext, dbUser.Id);

            var models = service.GetAll(user);

            Assert.Equal(dbUser.TestDrives.First().Id, models.First().Id);
            Assert.Single(models);
        }

        private ITestDriveService GetService(
            ApplicationDbContext dbContext, 
            string userId)
        {
            var mockedUserManager = CommonMockMethods.GetMockedUserManager();
            CommonMockMethods.SetupMockedUserManagerGetUserId(mockedUserManager, userId);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            return service;
        }
    }
}
