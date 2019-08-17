using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Models.PaginationModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminOptionsViewModel : BasePaginationModel
    {
        public OptionSortStrategyType SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public IEnumerable<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();
    }
}
