﻿using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public abstract class BaseCarViewModel : IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public bool IsNew { get; set; }

        public double Mileage { get; set; }

        public string ModelTypeName { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SeriesName { get; set; }

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, BaseCarViewModel>()
                .ForMember(dest => dest.IsNew, opt => opt.MapFrom(src => src is NewCar))
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src is UsedCar ? (src as UsedCar).Mileage : 0))
                .IncludeAllDerived();
        }
    }
}