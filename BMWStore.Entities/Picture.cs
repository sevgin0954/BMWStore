using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Picture
    {
        public string Id { get; set; }

        [Required]
        public string ExteriorId { get; set; }
        public Exterior Exterior { get; set; }
    }
}
