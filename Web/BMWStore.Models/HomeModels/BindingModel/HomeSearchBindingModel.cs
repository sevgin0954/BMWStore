using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Models.HomeModels.BindingModel
{
    public class HomeSearchBindingModel : IMapFrom<HomeSearchServiceModel>
    {
        public CarType SelectedCarType { get; set; } = CarType.NewCar;
        public IEnumerable<FilterTypeBindingModel> CarTypes { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedYear { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeBindingModel> Years { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedModelType { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeBindingModel> ModelTypes { get; set; } = new List<FilterTypeBindingModel>();

        public string SelectedPriceRange { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeBindingModel> PriceRanges { get; set; } = new List<FilterTypeBindingModel>();
    }
}
