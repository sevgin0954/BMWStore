using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Models.HomeModels.BindingModel;
using System.Collections.Generic;

namespace BMWStore.Models.HomeModels.ViewModels
{
    public class HomeViewModel
    {
        public IDictionary<string, string> TargetUrlsPublicIds { get; set; } = new Dictionary<string, string>();

        public HomeSearchBindingModel SerchModel { get; set; }

        public Dictionary<string, CarHomeViewModel> CarModelsCars { get; set; } = new Dictionary<string, CarHomeViewModel>();
    }
}
