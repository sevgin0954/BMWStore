using AutoMapper;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.OptionModels.ViewModels
{
    public class OptionConciseViewModel : IMapFrom<OptionServiceModel>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string OptionTypeName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionServiceModel, OptionConciseViewModel>()
                .ForMember(dest => dest.OptionTypeName, opt => opt.MapFrom(src => src.OptionType.Name));
        }
    }
}
