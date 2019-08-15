using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionTypeModels.BindingModels
{
    public class OptionTypeEditBindingModel
    {
        [Required]
        [BindNever]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
