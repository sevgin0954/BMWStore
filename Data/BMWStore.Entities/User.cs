using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(EntitiesConstants.UserNameMaxLength)]
        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(EntitiesConstants.UserNameMaxLength)]
        [MinLength(EntitiesConstants.UserNameMinLength)]
        [Required]
        public string LastName { get; set; }

        public ICollection<TestDrive> TestDrives { get; set; } = new List<TestDrive>();

        public ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();
    }
}