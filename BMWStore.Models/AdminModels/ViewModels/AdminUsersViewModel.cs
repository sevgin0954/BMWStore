using BMWStore.Common.Enums;
using BMWStore.Models.UserModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminUsersViewModel
    {
        public SortStrategyDirection SortStrategyDirection { get; set; }

        public UserSortStrategyType SortStrategyType { get; set; }

        public IEnumerable<UserAdminViewModel> Users { get; set; } = new List<UserAdminViewModel>();
    }
}
