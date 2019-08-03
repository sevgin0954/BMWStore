using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class BanUserAsyncTests : BaseAdminUsersServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public BanUserAsyncTests(BaseTestFixture baseTest)
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

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.BanUserAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithAdmin_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbAdmin = this.SeedAdminWithRole(dbContext);
            CommonSeedTestMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.BanUserAsync(dbAdmin.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithBannedUser_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = this.SeedUserWithRole(dbContext);
            this.BanUser(dbContext, dbUser);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.BanUserAsync(dbUser.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithNotBanned_ShouldBanUser()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = this.SeedUserWithRole(dbContext);

            await service.BanUserAsync(dbUser.Id);

            Assert.True(dbUser.LockoutEnd > DateTime.UtcNow);
        }
    }
}
