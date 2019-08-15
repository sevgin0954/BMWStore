using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionTypeModels.BindingModels
{
    public class OptionTypeEditBindingModel : IMapFrom<OptionType>, IMapTo<OptionType>
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
