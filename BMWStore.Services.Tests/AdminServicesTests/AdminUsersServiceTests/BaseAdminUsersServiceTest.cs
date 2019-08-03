using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class BaseAdminUsersServiceTest
    {
        protected IAdminUsersService GetService(ApplicationDbContext dbContext)
        {
            var roleRepository = new RoleRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var service = new AdminUsersService(roleRepository, userRepository);

            return service;
        }

        protected void SeedUserAndAdmin(ApplicationDbContext dbContext)
        {
            this.SeedAdminWithRole(dbContext);
            this.SeedUserWithRole(dbContext);

            dbContext.SaveChanges();
        }

        protected User SeedAdminWithRole(ApplicationDbContext dbContext)
        {
            var dbAdminRole = CommonSeedTestMethods.SeedAdminRole(dbContext);
            var dbAdmin = new User();
            dbAdmin.Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = dbAdminRole.Id
            });

            dbContext.Users.Add(dbAdmin);
            dbContext.SaveChanges();

            return dbAdmin;
        }

        protected User SeedUserWithRole(ApplicationDbContext dbContext)
        {
            var dbUserRole = CommonSeedTestMethods.SeedUserRole(dbContext);
            var dbUser = new User();
            dbUser.Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = dbUserRole.Id
            });

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            return dbUser;
        }
    }
}
