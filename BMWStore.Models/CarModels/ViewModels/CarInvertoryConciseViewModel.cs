using AutoMapper;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarInvertoryConciseViewModel : BaseCarScheduleTestDriveViewModel
    {
        public string PicturePublicId { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            base.CreateMappings(configuration);

            configuration.CreateMap<BaseCar, CarInvertoryConciseViewModel>()
                .ForMember(dest => dest.PicturePublicId, opt => opt.MapFrom(src => src.Pictures.First().PublicId));
        }
    }
}
