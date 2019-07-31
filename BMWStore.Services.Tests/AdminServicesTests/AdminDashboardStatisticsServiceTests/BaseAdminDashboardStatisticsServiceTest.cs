using BMWStore.Common.Constants;
using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public abstract class BaseAdminDashboardStatisticsServiceTest
    {
        public IAdminDashboardStatisticsService GetService(ApplicationDbContext dbContext)
        {
            var testDriveRepository = new TestDriveRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var roleRepository = new RoleRepository(dbContext);
            this.SeedRoles(roleRepository, dbContext);
            var carRepository = new CarRepository(dbContext);
            var service = new AdminDashboardStatisticsService(testDriveRepository, userRepository, roleRepository, carRepository);

            return service;
        }

        private void SeedRoles(IRoleRepository roleRepository, DbContext dbContext)
        {
            var userRole = new IdentityRole()
            {
                Name = WebConstants.UserRoleName,
                NormalizedName = WebConstants.UserRoleName.ToUpper()
            };
            roleRepository.Add(userRole);
            var adminRole = new IdentityRole()
            {
                Name = WebConstants.AdminRoleName,
                NormalizedName = WebConstants.AdminRoleName.ToUpper()
            };
            roleRepository.Add(adminRole);
            dbContext.SaveChanges();
        }
    }
}
