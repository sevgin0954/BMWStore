using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarTestDriveViewModel : IMapFrom<BaseCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PicturePublicId { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public string Vin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarTestDriveViewModel>()
                .ForMember(dest => dest.PicturePublicId, opt => opt.MapFrom(src => src.Pictures.First().PublicId));
        }
    }
}
