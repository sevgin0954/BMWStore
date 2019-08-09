using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.SeedDbServiceTests
{
    public abstract class BaseSeedDbServiceTest
    {
        protected ISeedDbService GetService(ApplicationDbContext dbContext)
        {
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            var statusRepository = new StatusRepository(dbContext);
            var userManager = CommonMockTestMethods.GetMockedUserManager().Object;
            var service = new SeedDbService(userRepository, roleRepository, statusRepository, userManager);

            return service;
        }
    }
}
