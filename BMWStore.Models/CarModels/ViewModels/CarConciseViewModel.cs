using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarConciseViewModel : IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public double Mileage { get; set; }

        public string ModelTypeName { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public string SeriesName { get; set; }

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewCar, CarConciseViewModel>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Pictures.First().Url));

            configuration.CreateMap<UsedCar, CarConciseViewModel>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Pictures.First().Url));
        }
    }
}
