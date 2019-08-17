using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Tests.Common.MockTestMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;
using Xunit;

namespace BMWStore.Services.Tests.SeedDbUsersServiceTests
{
    public class SeedUserAsyncTests : BaseSeedDbUsersServiceTest
    {
        [Fact]
        public async void WithAlreadyExistingEmail_ShouldNotCreateNewUser()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            var dbRole = SeedRolesMethods.SeedAdminRole(dbContext);
            var dbUser = SeedUsersMethods.SeedUser(dbContext, Guid.NewGuid().ToString());
            var service = this.GetService(dbContext, mockedUserManager.Object);

            await service.SeedUserAsync("", dbUser.Email, dbRole.Name);

            mockedUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void WithNonExistingRole_ShouldThrowExceptino()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            var dbUser = SeedUsersMethods.SeedUser(dbContext, Guid.NewGuid().ToString());
            var service = this.GetService(dbContext, mockedUserManager.Object);
            var nonExistingRoleName = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.SeedUserAsync("", "", nonExistingRoleName));
            Assert.Equal(ErrorConstants.RoleNotFound, exception.Message);
        }

        [Fact]
        public async void WithCorrectParameters_ShouldCreateNewUser()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            var dbRole = SeedRolesMethods.SeedUserRole(dbContext);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            await service.SeedUserAsync("", "", dbRole.Name);

            mockedUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void WithCorrectParameters_ShouldAddNewUserToRole()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            var dbRole = SeedRolesMethods.SeedUserRole(dbContext);
            var service = this.GetService(dbContext, mockedUserManager.Object);

            await service.SeedUserAsync("", "", dbRole.Name);

            mockedUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
