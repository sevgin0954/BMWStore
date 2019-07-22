using BMWStore.Common.Enums;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarInvertoryModels.ViewModels
{
    public class NewCarsInvertoryViewModel
    {
        public CarSortStrategyType SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
