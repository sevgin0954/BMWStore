using BMWStore.Common.Constants;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.ModelTypeModels.BindingModels
{
    public class ModelTypeBindingModel : IMapFrom<ModelTypeServiceModel>, IMapTo<ModelTypeServiceModel>
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.ModelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}