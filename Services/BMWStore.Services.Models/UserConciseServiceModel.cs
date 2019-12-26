using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class UserConciseServiceModel : IdentityUser, IMapFrom<User>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TestDrivesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserConciseServiceModel>()
                .ForMember(dest => dest.TestDrivesCount, opt => opt.MapFrom(src => src.TestDrives.Count()));
        }
    }
}