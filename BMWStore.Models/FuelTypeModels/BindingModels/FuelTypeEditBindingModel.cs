using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.FuelTypeModels.BindingModels
{
    public class FuelTypeEditBindingModel
    {
        [Required]
        [BindNever]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
