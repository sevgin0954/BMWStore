using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarConciseViewModel : IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public bool IsNew { get; set; }

        public double Mileage { get; set; }

        public string ModelTypeName { get; set; }

        public string Name { get; set; }

        public string PicturePublicId { get; set; }

        public decimal Price { get; set; }

        public string SeriesName { get; set; }

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarConciseViewModel>()
                .ForMember(dest => dest.PicturePublicId, opt => opt.MapFrom(src => src.Pictures.First().PublicId))
                .ForMember(dest => dest.IsNew, opt => opt.MapFrom(src => src is NewCar))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src is UsedCar ? (src as UsedCar).Mileage : 0))
                .IncludeAllDerived();
        }
    }
}
