using BMWStore.Common.Enums;
using BMWStore.Models.CarModels.ViewModels;

namespace BMWStore.Models.CarInvertoryModels.ViewModels
{
    public class CarInvertoryViewModel
    {
        public CarViewModel Car { get; set; }

        public bool isNew { get; set; }

        public string Year { get; set; }

        public string ModelTypeName { get; set; }

        public string ModelTypeId { get; set; }
    }
}
