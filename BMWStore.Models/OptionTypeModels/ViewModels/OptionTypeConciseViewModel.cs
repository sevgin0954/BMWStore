using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.OptionTypeModels.ViewModels
{
    public class OptionTypeConciseViewModel : IMapFrom<OptionType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int OptionsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionType, OptionTypeConciseViewModel>()
                .ForMember(dest => dest.OptionsCount, opt => opt.MapFrom(src => src.Options.Count));
        }
    }
}
