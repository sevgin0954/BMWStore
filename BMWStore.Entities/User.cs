using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class User : IdentityUser
    {
        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}