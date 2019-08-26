using BMWStore.Common.Constants;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SeriesModels.BindingModels
{
    public class SeriesBindingModel : IMapFrom<SeriesServiceModel>, IMapTo<SeriesServiceModel>
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
