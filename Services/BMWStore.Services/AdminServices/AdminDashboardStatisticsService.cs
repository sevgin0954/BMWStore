using BMWStore.Common.Constants;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminDashboardStatisticsService : IAdminDashboardStatisticsService
    {
        private readonly ITestDriveRepository testDriveRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ICarRepository carRepository;

        public AdminDashboardStatisticsService(
            ITestDriveRepository testDriveRepository, 
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICarRepository carRepository)
        {
            this.testDriveRepository = testDriveRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.carRepository = carRepository;
        }

        public async Task<DashboardStatisticsServiceModel> GetStatisticsAsync()
        {
            var totalNewCarsTestDrivesCount = await this.testDriveRepository
                .CountAsync(o => o.Car is NewCar);
            var newCarsTestDrivesFromPast24HoursCount = await this.testDriveRepository
                .CountAsync(uc => IsScheduledInLast24Hours(uc) && uc.Car is NewCar);
            var totalTestDrivesFromPast24Hours = await this.testDriveRepository
                .CountAsync(uc => IsScheduledInLast24Hours(uc));

            var dbUserRoleId = await this.roleRepository
                .GetIdByNameAsync(WebConstants.UserRoleName);
            var totalUsersCount = await this.userRepository
                .CountAsync(u => u.Roles.Any(r => r.RoleId == dbUserRoleId));

            var usedCarsOrderesCount = await this.testDriveRepository
                .CountAsync(o => o.Car is UsedCar);
            var UsedCarsOrderedFromPast24HoursCount = await this.testDriveRepository
                .CountAsync(uc => IsScheduledInLast24Hours(uc) && uc.Car is UsedCar);

            var totalNewCarsCount = await this.carRepository
                .CountAsync(c => c is NewCar);
            var totalUsedCarsCount = await this.carRepository
                .CountAsync(c => c is UsedCar);

            var model = new DashboardStatisticsServiceModel()
            {
                TotalNewCarsTestDrivesCount = totalNewCarsTestDrivesCount,
                NewCarsTestDrivesFromPast24HoursCount = newCarsTestDrivesFromPast24HoursCount,
                TotalTestDrivesFromPast24HoursCount = totalTestDrivesFromPast24Hours,
                TotalUsersCount = totalUsersCount,
                TotalUsedCarsTestDrivesCount = usedCarsOrderesCount,
                UsedCarsTestDrivesFromPast24HoursCount = UsedCarsOrderedFromPast24HoursCount,
                NewCarsCount = totalNewCarsCount,
                UsedCarsCount = totalUsedCarsCount
            };

            return model;
        }

        private bool IsScheduledInLast24Hours(TestDrive testDrive)
        {
            return DateTime.UtcNow.AddDays(-1) <= testDrive.ScheduleDate;
        }
    }
}
