using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class OptionType : BaseEntity, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public ICollection<Option> Options { get; set; } = new List<Option>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OptionType, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
