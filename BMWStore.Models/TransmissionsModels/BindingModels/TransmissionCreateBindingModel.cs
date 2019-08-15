using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.TransmissionsModels.BindingModels
{
    public class TransmissionCreateBindingModel : IMapTo<Transmission>
    {
        [MaxLength(EntitiesConstants.TransmissionNameMaxLength)]
        [MinLength(EntitiesConstants.TransmissionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.TransmissionMaxPrice)]
        [Required]
        public decimal Price { get; set; }
    }
}
