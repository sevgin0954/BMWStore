using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.PaginationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace BMWStore.Models.CarInventoryModels.ViewModels
{
    public class CarsInventoryViewModel : BasePaginationModel
    {
        [JsonProperty("SortStrategyType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Enum SortStrategyType { get; set; }

        [JsonProperty("SortStrategyDirection")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortStrategyDirection SortStrategyDirection { get; set; }

        public List<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<CarInventoryConciseViewModel> Cars { get; set; } = new List<CarInventoryConciseViewModel>();

        public int TotalCarsCount { get; set; }
    }
}