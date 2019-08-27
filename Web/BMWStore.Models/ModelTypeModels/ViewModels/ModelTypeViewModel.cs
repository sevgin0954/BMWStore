using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.ModelTypeModels.ViewModels
{
    public class ModelTypeViewModel : IMapFrom<ModelTypeServiceModel>
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }
    }
}
