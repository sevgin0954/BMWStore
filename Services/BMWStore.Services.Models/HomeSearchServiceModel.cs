using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class HomeSearchServiceModel
    {
        public CarType SelectedCarType { get; set; } = CarType.NewCar;
        public IEnumerable<FilterTypeServiceModel> CarTypes { get; set; } = new List<FilterTypeServiceModel>();

        public string SelectedYear { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeServiceModel> Years { get; set; } = new List<FilterTypeServiceModel>();

        public string SelectedModelType { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeServiceModel> ModelTypes { get; set; } = new List<FilterTypeServiceModel>();

        public string SelectedPriceRange { get; set; } = WebConstants.AllFilterTypeModelValue;
        public IEnumerable<FilterTypeServiceModel> PriceRanges { get; set; } = new List<FilterTypeServiceModel>();
    }
}