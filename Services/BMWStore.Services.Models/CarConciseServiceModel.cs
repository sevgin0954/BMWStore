using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class CarConciseServiceModel : IMapFrom<BaseCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FuelTypeName { get; set; }

        public double HoursePower { get; set; }

        public string ModelTypeId { get; set; }

        public double Mileage { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PicturePublicId { get; set; }

        public Type CarType { get; set; }

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarConciseServiceModel>()
                .ForMember(dest => dest.PicturePublicId, opt => opt.MapFrom(src => src.Pictures.First().PublicId))
                .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => src.GetType()))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src is UsedCar ? (src as UsedCar).Mileage : 0));
        }
    }
}
