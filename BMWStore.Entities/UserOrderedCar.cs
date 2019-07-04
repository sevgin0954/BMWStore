using System;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class UserOrderedCar
    {
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}