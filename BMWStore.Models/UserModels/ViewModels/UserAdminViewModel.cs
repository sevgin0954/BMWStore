namespace BMWStore.Models.UserModels.ViewModels
{
    public class UserAdminViewModel
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Id { get; set; }

        public bool IsBanned { get; set; }

        public int OrdersCount { get; set; }

        public string PhoneNumber { get; set; }
    }
}
