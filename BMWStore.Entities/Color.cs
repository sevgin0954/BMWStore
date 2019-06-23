using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Color
    {
        public string Id { get; set; }

        public ICollection<Exterior> Exteriors { get; set; } = new List<Exterior>();

        [MinLength(EntitiesConstants.ColorNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.ColorMaxPrice)]
        [Required]
        public decimal Price { get; set; }
    }
}