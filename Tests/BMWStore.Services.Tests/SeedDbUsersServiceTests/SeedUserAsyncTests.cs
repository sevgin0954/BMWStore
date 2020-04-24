using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.MockMethods;
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
            var mockedUserManager = CommonGetMockMethods.GetUserManager();
            var dbUser = SeedUsersMethods.SeedUser(dbContext, Guid.NewGuid().ToString());
            var service = this.GetService(dbContext, mockedUserManager.Object);

            var dbRole = SeedRolesMethods.SeedAdminRole(dbContext);
            var serviceModel = new UserServiceModel();
            serviceModel.Email = dbUser.Email;

            await service.SeedUserAsync(serviceModel, "", dbRole.Name);

            mockedUserManager.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }
    }
}
