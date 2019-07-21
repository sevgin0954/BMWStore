using BMWStore.Common.Constants;
using BMWStore.Data;
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
        private readonly ApplicationDbContext dbContext;

        public AdminDashboardStatisticsService(IBMWStoreUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        public async Task<AdminDashboardStatisticsViewModel> GetStatisticsAsync()
        {
            var newCarsOrderedCount = await this.unitOfWork.Orders
                .CountAsync(o => o.Car is NewCar);
            var newCarsOrderedFromPast24HoursCount = await this.unitOfWork.Orders
                .CountAsync(uc => IsOrderedInLast24Hours(uc) && uc.Car is NewCar);
            var orderesFromPast24Hours = await this.unitOfWork.Orders
                .CountAsync(uc => IsOrderedInLast24Hours(uc));

            var dbUserRoleId = await this.unitOfWork.Roles
                .GetIdByNameAsync(WebConstants.UserRoleName);
            var totalUsersCount = await this.unitOfWork.Users
                .CountByRole(dbUserRoleId);

            var totalOrdersCount = await this.unitOfWork.Orders
                .CountAllAsync();
            var usedCarsOrderesCount = await this.unitOfWork.Orders
                .CountAsync(o => o.Car is UsedCar);
            var UsedCarsOrderedFromPast24HoursCount = await this.unitOfWork.Orders
                .CountAsync(uc => IsOrderedInLast24Hours(uc) && uc.Car is UsedCar);

            var model = new AdminDashboardStatisticsViewModel()
            {
                NewCarsOrdersCount = newCarsOrderedCount,
                NewCarsOrderedFromPast24HoursCount = newCarsOrderedFromPast24HoursCount,
                OrdersFromPast24HoursCount = orderesFromPast24Hours,
                TotalUsersCount = totalUsersCount,
                TotalOrdersCount = totalOrdersCount,
                UsedCarsOrdersCount = usedCarsOrderesCount,
                UsedCarsOrderedFromPast24HoursCount = UsedCarsOrderedFromPast24HoursCount
            };

            return model;
        }

        private bool IsOrderedInLast24Hours(Order order)
        {
            return (order.OrderDate - DateTime.UtcNow).Hours <= 24;
        }
    }
}
