using BMWStore.Common.Enums;
using BMWStore.Models.TestDriveModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminTestDrivesViewModel
    {
        public SortStrategyDirection SortDirection { get; set; }

        public AdminTestDrivesSortStrategyType SortStrategyType { get; set; }

        public IEnumerable<TestDriveViewModel> TestDrives { get; set; } = new List<TestDriveViewModel>();
    }
}
