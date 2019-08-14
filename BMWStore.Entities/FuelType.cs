using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class FuelType : IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FuelType, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}