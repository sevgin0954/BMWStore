using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.ModelTypeModels.BindingModels
{
    public class ModelTypeEditBindingModel : IMapFrom<ModelType>
    {
        [BindNever]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.ModelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
