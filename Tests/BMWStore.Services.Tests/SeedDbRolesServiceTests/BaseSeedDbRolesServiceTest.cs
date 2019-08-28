using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.SeedDbRolesServiceTests
{
    public abstract class BaseSeedDbRolesServiceTest : BaseTest
    {
        public ISeedDbRolesService GetService(ApplicationDbContext dbContext)
        {
            var roleRepository = new RoleRepository(dbContext);
            var service = new SeedDbRolesService(roleRepository);

            return service;
        }
    }
}
