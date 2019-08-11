using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarConciseViewModel : BaseCarViewModel, IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public string PicturePublicId { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarConciseViewModel>()
                .ForMember(dest => dest.PicturePublicId, opt => opt.MapFrom(src => src.Pictures.First().PublicId))
                .IncludeAllDerived();

        }
    }
}
