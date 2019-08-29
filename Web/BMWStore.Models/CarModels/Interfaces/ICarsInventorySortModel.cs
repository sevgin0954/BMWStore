using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.CarModels.ViewModels;
using System;
using System.Collections.Generic;

namespace BMWStore.Models.CarModels.Interfaces
{
    public interface ICarsInventorySortModel
    {
        Enum SortStrategyType { get; set; }

        SortStrategyDirection SortStrategyDirection { get; set; }

        IEnumerable<CarInventoryConciseViewModel> Cars { get; set; }
    }
}