using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.PaginationModels;
using BMWStore.Models.TestDriveModels.BindingModels;
using System;
using System.Collections.Generic;

namespace BMWStore.Models.CarInvertoryModels.ViewModels
{
    public class CarsInvertoryViewModel : BasePaginationModel
    {
        public Enum SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public List<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<CarInvertoryConciseViewModel> Cars { get; set; } = new List<CarInvertoryConciseViewModel>();

        public ScheduleTestDriveBindingModel TestDriveModel { get; set; }
    }
}