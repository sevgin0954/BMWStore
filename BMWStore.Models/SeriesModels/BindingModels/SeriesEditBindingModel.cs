using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SeriesModels.BindingModels
{
    public class SeriesEditBindingModel : IMapFrom<Series>, IMapTo<Series>
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
