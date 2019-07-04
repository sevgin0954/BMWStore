using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services
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
            var newCarsOrderedCount = await this.unitOfWork.NewCars.CountAllAsync();
            var newCarsOrderedFromPast24HoursCount = await this.unitOfWork.UsersOrderedCars
                .CountAsync(uc => IsOrderedInLast24Hours(uc) && uc.Car is NewCar);
            var orderesFromPast24Hours = await this.unitOfWork.UsersOrderedCars
                .CountAsync(uc => IsOrderedInLast24Hours(uc));
            var totalUsersCount = await this.unitOfWork.Users.CountAllAsync();
            var totalOrdersCount = await this.unitOfWork.UsersOrderedCars.CountAllAsync();
            var usedCarsOrderesCount = await this.unitOfWork.UsedCars.CountAllAsync();
            var UsedCarsOrderedFromPast24HoursCount = await this.unitOfWork.UsersOrderedCars
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

        private bool IsOrderedInLast24Hours(UserOrderedCar userOrderedCar)
        {
            return (userOrderedCar.OrderDate - DateTime.UtcNow).Hours <= 24;
        }
    }
}
