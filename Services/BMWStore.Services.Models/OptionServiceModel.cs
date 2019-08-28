using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class OptionServiceModel : IMapTo<Option>, IMapFrom<Option>, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ICollection<CarOptionServiceModel> CarsOptions { get; set; } = new List<CarOptionServiceModel>();

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string OptionTypeId { get; set; }
        public OptionTypeServiceModel OptionType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionServiceModel, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }
    }
}
