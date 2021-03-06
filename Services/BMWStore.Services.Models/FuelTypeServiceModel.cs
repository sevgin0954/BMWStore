﻿using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class FuelTypeServiceModel : IMapTo<SelectListItem>, IMapFrom<FuelType>, IMapTo<FuelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FuelTypeServiceModel, FuelType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<FuelType, FuelTypeServiceModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count()));

            configuration.CreateMap<FuelTypeServiceModel, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
