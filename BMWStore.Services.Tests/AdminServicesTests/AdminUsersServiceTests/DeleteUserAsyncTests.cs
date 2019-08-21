﻿using BMWStore.Common.Constants;
using BMWStore.Tests.Common.MockTestMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class DeleteUserAsyncTests : BaseAdminUsersServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.DeleteUserAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithAdmin_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerIsInRoleAsync(mockedUserManager, false);
            var service = this.GetService(dbContext, mockedUserManager.Object);
            var dbAdmin = SeedUsersMethods.SeedAdminWithRole(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.DeleteUserAsync(dbAdmin.Id));
            Assert.Equal(ErrorConstants.IncorrectUser, exception.Message);
        }

        [Fact]
        public async void WithUser_ShoudDeleteUser()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = CommonMockTestMethods.GetMockedUserManager();
            CommonMockTestMethods.SetupMockedUserManagerIsInRoleAsync(mockedUserManager, true);
            var service = this.GetService(dbContext, mockedUserManager.Object);
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);

            await service.DeleteUserAsync(dbUser.Id);

            Assert.Empty(dbContext.Users);
        }
    }
}
