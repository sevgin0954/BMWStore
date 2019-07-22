using System.Collections.Generic;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarViewModel : CarConciseViewModel
    {
        public string EngineName { get; set; }

        public string ExteriorColorName { get; set; }

        public double FuelConsumation_City_Litres_100Km { get; set; }

        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        public IEnumerable<string> OptionsName { get; set; } = new List<string>();

        public string TransmissionName { get; set; }
    }
}
