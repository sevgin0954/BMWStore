using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class OptionTypeServiceModel : IMapFrom<OptionType>, IMapTo<OptionType>, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int OptionsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionType, OptionTypeServiceModel>()
                .ForMember(dest => dest.OptionsCount, opt => opt.MapFrom(src => src.Options.Count()));

            configuration.CreateMap<OptionTypeServiceModel, OptionType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<OptionTypeServiceModel, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
    }
    }
}