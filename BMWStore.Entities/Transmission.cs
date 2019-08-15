using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Transmission : BaseEntity, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public ICollection<Engine> Engines { get; set; } = new List<Engine>();

        [MaxLength(EntitiesConstants.TransmissionNameMaxLength)]
        [MinLength(EntitiesConstants.TransmissionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.TransmissionMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Transmission, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }
    }
}