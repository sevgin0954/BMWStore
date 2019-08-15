using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Series : BaseEntity, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Series, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}