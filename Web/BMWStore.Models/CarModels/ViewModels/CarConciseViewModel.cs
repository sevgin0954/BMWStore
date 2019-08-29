using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarConciseViewModel : BaseCarViewModel, IMapFrom<BaseCarServiceModel>
    {
        public string PicturePublicId { get; set; }
    }
}
