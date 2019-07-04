namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminDashboardStatisticsViewModel
    {
        public int NewCarsOrdersCount { get; set; }

        public int NewCarsOrderedFromPast24HoursCount { get; set; }

        public int OrdersFromPast24HoursCount { get; set; }

        public int TotalUsersCount { get; set; }

        public int TotalOrdersCount { get; set; }

        public int UsedCarsOrdersCount { get; set; }

        public int UsedCarsOrderedFromPast24HoursCount { get; set; }
    }
}