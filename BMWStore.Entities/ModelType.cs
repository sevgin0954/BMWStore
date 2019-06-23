using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class ModelType
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}