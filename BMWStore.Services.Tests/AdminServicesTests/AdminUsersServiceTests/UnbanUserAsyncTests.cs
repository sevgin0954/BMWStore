using BMWStore.Common.Constants;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class UnbanUserAsyncTests : BaseAdminUsersServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();
            SeedRolesMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithAdmin_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbAdmin = SeedUsersMethods.SeedAdminWithRole(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(dbAdmin.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithNotBannedUser_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.UnbanUserAsync(dbUser.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithBannedUser_ShouldUnbanUser()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);
            this.BanUser(dbContext, dbUser);

            await service.UnbanUserAsync(dbUser.Id);

            Assert.Null(dbUser.LockoutEnd);
        }
    }
}
