using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarsFilterViewModel : IMapFrom<CarsFilterServiceModel>
    {
        public List<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Series { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public List<FilterTypeBindingModel> Prices { get; set; } = new List<FilterTypeBindingModel>();
    }
}
