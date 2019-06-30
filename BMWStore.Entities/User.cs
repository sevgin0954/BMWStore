using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BMWStore.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserBoughtCar> BoughtCars { get; set; } = new List<UserBoughtCar>();
    }
}
