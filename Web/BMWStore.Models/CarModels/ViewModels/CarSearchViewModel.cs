using System;
using System.Collections.Generic;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.CarModels.Interfaces;
using BMWStore.Models.PaginationModels;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarSearchViewModel : BasePaginationModel, ICarsInventorySortModel
    {
        public Enum SortStrategyType { get; set; }
        public SortStrategyDirection SortStrategyDirection { get; set; }
        public IEnumerable<CarInventoryConciseViewModel> Cars { get; set; }
    }
}
