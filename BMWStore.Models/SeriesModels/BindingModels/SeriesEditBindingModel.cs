using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SeriesModels.BindingModels
{
    public class SeriesEditBindingModel
    {
        [Required]
        [BindNever]
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
