using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class BaseAdminUsersServiceTest : BaseTest
    {
        protected IAdminUsersService GetService(ApplicationDbContext dbContext)
        {
            var roleRepository = new RoleRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var sortCookieService = new Mock<ICookiesService>().Object;
            var readService = new ReadService(dbContext);
            var service = new AdminUsersService(roleRepository, userRepository, sortCookieService, readService);

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
