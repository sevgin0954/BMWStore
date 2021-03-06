﻿using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class EngineServiceModel : IMapTo<SelectListItem>, IMapFrom<Engine>, IMapTo<Engine>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string TransmissionId { get; set; }
        public TransmissionServiceModel Transmission { get; set; }

        public int Weight_Kg { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EngineServiceModel, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            configuration.CreateMap<EngineServiceModel, Engine>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<Engine, EngineServiceModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count()));
        }
    }
}