﻿using AutoMapper;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Models.OptionModels.ViewModels
{
    public class OptionViewModel : IMapFrom<OptionServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OptionTypeName { get; set; }

        public decimal Price { get; set; }

        public int CarsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionServiceModel, OptionViewModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.CarsOptions.Count()))
                .ForMember(dest => dest.OptionTypeName, opt => opt.MapFrom(src => src.OptionType.Name));
        }
    }
}
