using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class NewCarConciseViewModel : IMapFrom<NewCar>
    {
        public string EngineName { get; set; }

        public string ExteriorColorName { get; set; }

        public double FuelConsumation_City_Litres_100Km { get; set; }

        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> OptionsName { get; set; } = new List<string>();

        public decimal Price { get; set; }

        public string TransmissionName { get; set; }

        public string Vin { get; set; }

        public string Year { get; set; }
    }
}
