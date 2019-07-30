using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public abstract class BaseAdminDashboardStatisticsServiceTest
    {
        public IAdminDashboardStatisticsService GetService(DbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            var service = new AdminDashboardStatisticsService(testDriveRepository, userRepository, roleRepository);

            return service;
        }
    }
}
