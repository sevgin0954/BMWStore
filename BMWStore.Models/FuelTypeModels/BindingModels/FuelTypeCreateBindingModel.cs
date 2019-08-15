using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.FuelTypeModels.BindingModels
{
    public class FuelTypeCreateBindingModel : IMapTo<FuelType>
    {
        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
