using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.SeedDbUsersServiceTests
{
    public abstract class BaseSeedDbUsersServiceTest : BaseTest
    {
        public ISeedDbUsersService GetService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            var service = new SeedDbUsersService(userRepository, roleRepository, userManager);

            return service;
        }
    }
}
