using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMWStore.Models.HomeModels.BindingModel
{
    public class HomeSearchBindingModel
    {
        public string SelectedCarInventory { get; set; }
        public IEnumerable<SelectListItem> CarInverntories { get; set; } = new List<SelectListItem>();

        public string SelectedYear { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();

        public string SelectedModelType { get; set; }
        public IEnumerable<SelectListItem> ModelTypes { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> CarPrices { get; set; } = new List<SelectListItem>();
    }
}
