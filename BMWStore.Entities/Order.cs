using BMWStore.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Order
    {
        public string Id { get; set; }

        [MinLength(EntitiesConstants.OrderAddressMinLength)]
        [Required]
        public string Address { get; set; }

        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}