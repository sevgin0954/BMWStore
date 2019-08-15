using AutoMapper;
using BMWStore.Common.Constants;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Engine : BaseEntity, IMapTo<SelectListItem>, IHaveCustomMappings
    {
        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MaxLength(EntitiesConstants.EngineeNameMaxLength)]
        [MinLength(EntitiesConstants.EngineNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.EngineMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }

        [Range(EntitiesConstants.EngineMinWeight_Kg, EntitiesConstants.EngineMaxWeight_Kg)]
        [Required]
        public int Weight_Kg { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Engine, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}