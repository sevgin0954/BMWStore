using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarsFilterViewModel
    {
        public ICollection<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public ICollection<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public ICollection<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public ICollection<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();
    }
}
