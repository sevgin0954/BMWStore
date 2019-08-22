using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionTypeModels.BindingModels
{
    public class OptionTypeBindingModel : IMapFrom<OptionType>, IMapTo<OptionType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionTypeBindingModel, OptionType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
