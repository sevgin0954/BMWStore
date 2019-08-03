using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class UnbanUserAsyncTests : BaseAdminUsersServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public UnbanUserAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();
            CommonSeedTestMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithAdmin_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbAdmin = this.SeedAdminWithRole(dbContext);
            CommonSeedTestMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(dbAdmin.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithNotBannedUser_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = this.SeedUserWithRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(dbUser.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithBannedUser_ShouldUnbanUser()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = this.SeedUserWithRole(dbContext);
            this.BanUser(dbContext, dbUser);

            await service.UnbanUserAsync(dbUser.Id);

            Assert.Null(dbUser.LockoutEnd);
        }
    }
}
