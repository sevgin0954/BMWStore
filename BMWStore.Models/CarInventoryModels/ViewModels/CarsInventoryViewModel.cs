﻿using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.PaginationModels;
using BMWStore.Models.TestDriveModels.BindingModels;
using System;
using System.Collections.Generic;

namespace BMWStore.Models.CarInventoryModels.ViewModels
{
    public class CarsInventoryViewModel : BasePaginationModel
    {
        public Enum SortStrategyType { get; set; }

        public SortStrategyDirection SortStrategyDirection { get; set; }

        public List<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<CarInventoryConciseViewModel> Cars { get; set; } = new List<CarInventoryConciseViewModel>();

        public ScheduleTestDriveBindingModel TestDriveModel { get; set; }
    }
}