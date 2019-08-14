using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionTypeModels.BindingModels
{
    public class OptionTypeCreateBindingModel : IMapTo<OptionType>
    {
        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}