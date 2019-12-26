using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Identity;
using System;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class BaseAdminUsersServiceTest : BaseTest
    {
        protected IAdminUsersService GetService(ApplicationDbContext dbContext)
        {
            var userManager = CommonGetMockMethods.GetUserManager().Object;
            var service = this.GetService(dbContext, userManager);

            return service;
        }

        protected IAdminUsersService GetService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            var roleRepository = new RoleRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var service = new AdminUsersService(
                userManager,
                roleRepository,
                userRepository);

            return service;
        }

        protected void SeedUserAndAdmin(ApplicationDbContext dbContext)
        {
            SeedUsersMethods.SeedUserWithRole(dbContext);
            SeedUsersMethods.SeedAdminWithRole(dbContext);
        }

        protected void BanUser(ApplicationDbContext dbContext, User user)
        {
            user.LockoutEnd = DateTime.UtcNow.AddDays(1);
            dbContext.SaveChanges();
        }
    }
}