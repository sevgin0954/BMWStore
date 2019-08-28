using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminDashboardStatisticsViewModel : IMapFrom<DashboardStatisticsServiceModel>
    {
        public int TotalNewCarsTestDrivesCount { get; set; }

        public int NewCarsTestDrivesFromPast24HoursCount { get; set; }

        public int TotalUsedCarsTestDrivesCount { get; set; }

        public int UsedCarsTestDrivesFromPast24HoursCount { get; set; }

        public int TotalTestDrivesFromPast24HoursCount { get; set; }

        public int TotalUsersCount { get; set; }

        public int NewCarsCount { get; set; }

        public int UsedCarsCount { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    }
}