using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.EngineModels.BindingModels
{
    public class AdminEngineEditBindingModel : IMapTo<Engine>, IMapFrom<Engine>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.EngineeNameMaxLength)]
        [MinLength(EntitiesConstants.EngineNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.EngineMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string SelectedTransmissionId { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; } = new List<SelectListItem>();

        [Display(Name = "Weight in kg")]
        [Range(EntitiesConstants.EngineMinWeight_Kg, EntitiesConstants.EngineMaxWeight_Kg)]
        [Required]
        public int Weight_Kg { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AdminEngineEditBindingModel, Engine>()
                .ForMember(dest => dest.TransmissionId, opt => opt.MapFrom(src => src.SelectedTransmissionId))
                .ReverseMap()
                .ForMember(dest => dest.Transmissions, opt => opt.MapFrom(src => new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Selected = true,
                        Value = src.TransmissionId,
                        Text = src.Transmission.Name
                    }
                }));
        }
    }
}
