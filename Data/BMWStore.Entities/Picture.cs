using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Picture
    {
        public string Id { get; set; }

        [Required]
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        [Required]
        public string PublicId { get; set; }
    }
}