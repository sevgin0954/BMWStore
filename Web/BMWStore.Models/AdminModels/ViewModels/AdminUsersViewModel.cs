using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.PaginationModels;
using BMWStore.Models.UserModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminUsersViewModel : BasePaginationModel
    {
        public SortStrategyDirection SortStrategyDirection { get; set; }

        public UserSortStrategyType SortStrategyType { get; set; }

        public IEnumerable<UserAdminViewModel> Users { get; set; } = new List<UserAdminViewModel>();
    }
}
