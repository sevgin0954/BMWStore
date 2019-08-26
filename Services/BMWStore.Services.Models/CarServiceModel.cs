using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class CarServiceModel : IMapTo<BaseCar>, IMapTo<UsedCar>, IMapFrom<BaseCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public double Acceleration_0_100Km { get; set; }

        public int CO2Emissions { get; set; }

        public string ColorName { get; set; }

        public string Description { get; set; }

        public double Displacement { get; set; }

        public int DoorsCount { get; set; }

        public string EngineId { get; set; }
        public EngineServiceModel Engine { get; set; }

        public double FuelConsumation_City_Litres_100Km { get; set; }

        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        public string FuelTypeId { get; set; }
        public FuelTypeServiceModel FuelType { get; set; }

        public double HoursePower { get; set; }

        public string ModelTypeId { get; set; }
        public ModelTypeServiceModel ModelType { get; set; }

        public Type CarType { get; set; }

        public double Mileage { get; set; }

        public string Name { get; set; }

        public ICollection<CarOptionServiceModel> Options { get; set; } = new List<CarOptionServiceModel>();

        public ICollection<PictureServiceModel> Pictures { get; set; } = new List<PictureServiceModel>();

        public decimal Price { get; set; }

        public string SeriesId { get; set; }
        public SeriesServiceModel Series { get; set; }

        public decimal Torque { get; set; }

        public ICollection<TestDriveServiceModel> TestDrives { get; set; } = new List<TestDriveServiceModel>();

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public int Weight_Kg { get; set; }

        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarServiceModel, UsedCar>()
                .ForMember(dest => dest.Pictures, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<CarServiceModel, NewCar>()
                .ForMember(dest => dest.Pictures, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<BaseCar, CarServiceModel>()
                .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => src.GetType()))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src is UsedCar ? (src as UsedCar).Mileage : 0));
        }
    }
}
