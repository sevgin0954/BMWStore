using BMWStore.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Option
    {
        public string Id { get; set; }

        [MinLength(EntitiesConstants.OptionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.OptionNameMaxPrice)]
        [Required]
        public decimal Price { get; set; }
    }
}