using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.FuelTypeModels.ViewModels
{
    public class FuelTypeViewModel : IMapFrom<FuelTypeServiceModel>
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }
    }
}