using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetUserByIdAsyncTests : BaseAdminUsersServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetUserByIdAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnUser()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetUserByIdAsync(dbUser.Id);

            Assert.NotNull(model);
        }
    }
}