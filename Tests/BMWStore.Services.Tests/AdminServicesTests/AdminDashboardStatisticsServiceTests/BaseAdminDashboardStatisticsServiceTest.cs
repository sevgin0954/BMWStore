using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public abstract class BaseAdminDashboardStatisticsServiceTest : BaseTest
    {
        protected IAdminDashboardStatisticsService GetService(ApplicationDbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            this.SeedAllRoles(dbContext);
            var carRepository = new CarRepository(dbContext);
            var service = new AdminDashboardStatisticsService(testDriveRepository, userRepository, roleRepository, carRepository);

            return service;
        }

        protected IdentityRole AdminRole { get; private set; }

        protected IdentityRole UserRole { get; private set; }

        private void SeedAllRoles(ApplicationDbContext dbContext)
        {
            this.AdminRole = SeedRolesMethods.SeedAdminRole(dbContext);
            this.UserRole = SeedRolesMethods.SeedUserRole(dbContext);
        }
    }
}
