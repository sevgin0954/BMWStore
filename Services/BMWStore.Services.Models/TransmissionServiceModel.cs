using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMWStore.Services.Models
{
    public class TransmissionServiceModel : IMapTo<SelectListItem>, IMapFrom<Transmission>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TransmissionServiceModel, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));

            configuration.CreateMap<TransmissionServiceModel, Transmission>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
