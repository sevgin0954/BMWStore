using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Models.HomeModels.BindingModel
{
    public class HomeSearchBindingModel
    {
        public IEnumerable<FilterTypeBindingModel> CarInverntories { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public IEnumerable<FilterTypeBindingModel> CarPrices { get; set; } = new List<FilterTypeBindingModel>();
    }
}
