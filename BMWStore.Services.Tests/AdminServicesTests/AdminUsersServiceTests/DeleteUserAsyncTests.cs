using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class DeleteUserAsyncTests : BaseAdminUsersServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public DeleteUserAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            CommonSeedTestMethods.SeedUserRole(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.DeleteUserAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithAdmin_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbAdmin = this.SeedAdminWithRole(dbContext);
            CommonSeedTestMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.DeleteUserAsync(dbAdmin.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithUser_ShoudDeleteUser()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var dbUser = this.SeedUserWithRole(dbContext);

            await service.DeleteUserAsync(dbUser.Id);

            Assert.Empty(dbContext.Users);
        }
    }
}
