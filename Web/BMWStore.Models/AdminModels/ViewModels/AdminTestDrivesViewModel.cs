using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.PaginationModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminTestDrivesViewModel : BasePaginationModel
    {
        public SortStrategyDirection SortDirection { get; set; }

        public AdminTestDrivesSortStrategyType SortStrategyType { get; set; }

        public IEnumerable<TestDriveViewModel> TestDrives { get; set; } = new List<TestDriveViewModel>();
    }
}
