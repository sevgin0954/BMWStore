using BMWStore.Common.Constants;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionTypeModels.BindingModels
{
    public class OptionTypeBindingModel : IMapFrom<OptionTypeServiceModel>, IMapTo<OptionTypeServiceModel>
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.OptionTypeNameMaxLength)]
        [MinLength(EntitiesConstants.OptionTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
