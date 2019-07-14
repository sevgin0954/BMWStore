using BMWStore.Common.Enums;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminCarsViewModel
    {
        public SortStrategyDirection SortStrategyDirection { get; set; }

        public CarSortStrategyType SortStrategyType { get; set; }

        public IEnumerable<CarConciseViewModel> Cars { get; set; }
    }
}
