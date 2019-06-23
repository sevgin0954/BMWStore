using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Series
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}