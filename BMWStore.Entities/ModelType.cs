using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class ModelType : IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MaxLength(EntitiesConstants.ModelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ModelType, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }
    }
}