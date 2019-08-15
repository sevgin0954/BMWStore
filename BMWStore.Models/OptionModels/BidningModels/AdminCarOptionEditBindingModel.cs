using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionModels.BidningModels
{
    public class AdminCarOptionEditBindingModel : IMapFrom<Option>, IMapTo<Option>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.OptionNameMaxLength)]
        [MinLength(EntitiesConstants.OptionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.OptionNameMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OptionTypeId { get; set; }
        public IEnumerable<SelectListItem> OptionTypes { get; set; } = new List<SelectListItem>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AdminCarOptionEditBindingModel, Option>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
