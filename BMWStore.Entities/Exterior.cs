using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Exterior
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [Required]
        public string ColorId { get; set; }
        public Color Color { get; set; }

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}