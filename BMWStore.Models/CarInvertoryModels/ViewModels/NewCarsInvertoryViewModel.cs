using BMWStore.Common.Enums;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarInvertoryModels.ViewModels
{
    public class NewCarsInvertoryViewModel
    {
        public NewBaseCarSortStrategyType SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public IEnumerable<FilterTypeBindingModel> Years { get; set; }

        public IEnumerable<FilterTypeBindingModel> Series { get; set; }

        public IEnumerable<FilterTypeBindingModel> ModelTypes { get; set; }

        public IEnumerable<FilterTypeBindingModel> Prices { get; set; }

        public IEnumerable<CarConciseViewModel> Cars { get; set; }
    }
}
