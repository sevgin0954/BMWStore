using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.OptionModels.ViewModels
{
    public class OptionConciseViewModel : IMapFrom<Option>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string OptionTypeName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Option, OptionConciseViewModel>()
                .ForMember(dest => dest.OptionTypeName, opt => opt.MapFrom(src => src.OptionType.Name));
        }
    }
}
