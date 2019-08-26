using BMWStore.Common.Constants;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.TransmissionsModels.BindingModels
{
    public class TransmissionBindingModel : IMapTo<TransmissionServiceModel>
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.TransmissionNameMaxLength)]
        [MinLength(EntitiesConstants.TransmissionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.TransmissionMaxPrice)]
        [Required]
        public decimal Price { get; set; }
    }
}
