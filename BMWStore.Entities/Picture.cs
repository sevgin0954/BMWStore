using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Picture
    {
        public string Id { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}