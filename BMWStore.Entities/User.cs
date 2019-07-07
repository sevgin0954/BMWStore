using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class User : IdentityUser
    {
        [MinLength(EntitiesConstants.UserAddressMinLength)]
        [Required]
        public string Address { get; set; }

        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string LastName { get; set; }

        public ICollection<UserOrderedCar> OrderedCars { get; set; } = new List<UserOrderedCar>();
    }
}