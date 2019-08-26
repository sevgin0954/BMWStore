using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarHomeViewModel : IMapFrom<BaseCar>
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string PicturePublicId { get; set; }
    }
}
