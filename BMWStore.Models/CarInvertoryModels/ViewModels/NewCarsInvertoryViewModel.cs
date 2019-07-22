using BMWStore.Common.Enums;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarInvertoryModels.ViewModels
{
    public class NewCarsInvertoryViewModel
    {
        public NewBaseCarSortStrategyType SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public IEnumerable<CarConciseViewModel> Cars { get; set; }
    }
}
