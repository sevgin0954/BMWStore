using AutoMapper;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMWStore.Entities
{
    public class CarOption : IMapFrom<SelectListItem>, IHaveCustomMappings
    {
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        public string OptionId { get; set; }
        public Option Option { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SelectListItem, CarOption>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.Value));
        }
    }
}
