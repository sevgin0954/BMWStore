using BMWStore.Common.Constants;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.FuelTypeModels.BindingModels
{
    public class FuelTypeBindingModel : IMapTo<FuelTypeServiceModel>, IMapFrom<FuelTypeServiceModel>
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
