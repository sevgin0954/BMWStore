using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Models.CarInventoryModels.ViewModels
{
    public class CarInventoryViewModel
    {
        public CarViewModel Car { get; set; }

        public IEnumerable<OptionTypeViewModel> OptionTypes { get; set; } = new List<OptionTypeViewModel>();
    }
}