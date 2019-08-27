using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMWStore.Services.Models
{
    public class CarOptionServiceModel : IMapFrom<SelectListItem>, IMapTo<CarOption>, IMapFrom<CarOption>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CarId { get; set; }

        public string OptionId { get; set; }

        public string OptionName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SelectListItem, CarOptionServiceModel>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.Value));
        }
    }
}
