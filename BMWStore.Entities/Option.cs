using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Option : BaseEntity, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public ICollection<CarOption> CarsOptions { get; set; } = new List<CarOption>();

        [MaxLength(EntitiesConstants.OptionNameMaxLength)]
        [MinLength(EntitiesConstants.OptionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.OptionNameMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OptionTypeId { get; set; }
        public OptionType OptionType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Option, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }
    }
}