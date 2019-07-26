using BMWStore.Common.Constants;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminDashboardStatisticsService : IAdminDashboardStatisticsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminDashboardStatisticsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AdminDashboardStatisticsViewModel> GetStatisticsAsync()
        {
            var totalNewCarsTestDrivesCount = await this.unitOfWork.TestDrives
                .CountAsync(o => o.Car is NewCar);
            var newCarsTestDrivesFromPast24HoursCount = await this.unitOfWork.TestDrives
                .CountAsync(uc => IsScheduledInLast24Hours(uc) && uc.Car is NewCar);
            var totalTestDrivesFromPast24Hours = await this.unitOfWork.TestDrives
                .CountAsync(uc => IsScheduledInLast24Hours(uc));

            var dbUserRoleId = await this.unitOfWork.Roles
                .GetIdByNameAsync(WebConstants.UserRoleName);
            var totalUsersCount = await this.unitOfWork.Users
                .CountByRole(dbUserRoleId);

            var usedCarsOrderesCount = await this.unitOfWork.TestDrives
                .CountAsync(o => o.Car is UsedCar);
            var UsedCarsOrderedFromPast24HoursCount = await this.unitOfWork.TestDrives
                .CountAsync(uc => IsScheduledInLast24Hours(uc) && uc.Car is UsedCar);

            var model = new AdminDashboardStatisticsViewModel()
            {
                TotalNewCarsTestDrivesCount = totalNewCarsTestDrivesCount,
                NewCarsTestDrivesFromPast24HoursCount = newCarsTestDrivesFromPast24HoursCount,
                TotalTestDrivesFromPast24HoursCount = totalTestDrivesFromPast24Hours,
                TotalUsersCount = totalUsersCount,
                TotalUsedCarsTestDrivesCount = usedCarsOrderesCount,
                UsedCarsOrderedFromPast24HoursCount = UsedCarsOrderedFromPast24HoursCount
            };

            return model;
        }

        private bool IsScheduledInLast24Hours(TestDrive testDrive)
        {
            return (testDrive.ScheduleDate - DateTime.UtcNow).Hours <= 24;
        }
    }
}
