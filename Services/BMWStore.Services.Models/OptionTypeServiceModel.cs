using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class OptionTypeServiceModel : IMapFrom<OptionType>, IMapTo<OptionType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<OptionServiceModel> Options { get; set; } = new List<OptionServiceModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionServiceModel, OptionType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}