﻿using AutoMapper;
using BMWStore.Entities;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.TransmissionsModels.ViewModels
{
    public class TransmissionViewModel : IMapFrom<TransmissionServiceModel>, IHaveCustomMappings
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int EnginesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Transmission, TransmissionViewModel>()
                .ForMember(dest => dest.EnginesCount, opt => opt.MapFrom(src => src.Engines.Count));
        }
    }
}
