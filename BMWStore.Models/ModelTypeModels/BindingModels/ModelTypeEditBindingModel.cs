using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.ModelTypeModels.BindingModels
{
    public class ModelTypeEditBindingModel : IMapFrom<ModelType>, IMapTo<ModelType>
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.ModelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
