using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BMWStore.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserOrderedCar> OrderedCars { get; set; } = new List<UserOrderedCar>();
    }
}