using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Option
    {
        public string Id { get; set; }

        public ICollection<CarOption> CarsOptions { get; set; } = new List<CarOption>();

        [MaxLength(EntitiesConstants.OptionNameMaxLength)]
        [MinLength(EntitiesConstants.OptionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.OptionNameMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OptionTypeId { get; set; }
        public OptionType OptionType { get; set; }
    }
}