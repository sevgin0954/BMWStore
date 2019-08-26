using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class UserServiceModel : IdentityUser, IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<TestDriveServiceModel> TestDrives { get; set; } = new List<TestDriveServiceModel>();

        public ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();
    }
}
