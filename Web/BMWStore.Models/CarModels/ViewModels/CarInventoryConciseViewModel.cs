using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarInventoryConciseViewModel : BaseCarTestDriveViewModel, IMapFrom<CarConciseTestDriveServiceModel>
    {
        public string PicturePublicId { get; set; }
    }
}
