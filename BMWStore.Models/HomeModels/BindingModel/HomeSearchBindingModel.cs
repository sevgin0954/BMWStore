using BMWStore.Models.FilterModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMWStore.Models.HomeModels.BindingModel
{
    public class HomeSearchBindingModel
    {
        public string SelectedCarInventory { get; set; }
        public IEnumerable<FilterTypeBindingModel> CarInverntories { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedYear { get; set; }
        public IEnumerable<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedModelType { get; set; }
        public IEnumerable<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedPrice { get; set; }
        public IEnumerable<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();
    }
}
