using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public abstract class BaseAdminDashboardStatisticsServiceTest
    {
        public IAdminDashboardStatisticsService GetService(ApplicationDbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            CommonSeedTestMethods.SeedRoles(dbContext);
            var carRepository = new CarRepository(dbContext);
            var service = new AdminDashboardStatisticsService(testDriveRepository, userRepository, roleRepository, carRepository);

            return service;
        }
    }
}
