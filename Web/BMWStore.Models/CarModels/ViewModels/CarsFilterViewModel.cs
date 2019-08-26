using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarsFilterViewModel
    {
        public List<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();
    }
}
