using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.FuelTypeModels.BindingModels
{
    public class FuelTypeEditBindingModel : IMapFrom<FuelType>, IMapTo<FuelType>
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
