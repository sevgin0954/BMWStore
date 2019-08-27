using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.EngineModels.ViewModels
{
    public class EngineViewModel : IMapFrom<EngineServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TransmissionName { get; set; }

        public decimal Price { get; set; }

        public int Weight_Kg { get; set; }

        public int CarsCount { get; set; }
    }
}
