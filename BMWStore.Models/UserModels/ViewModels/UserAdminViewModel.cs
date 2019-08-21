using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Models.UserModels.ViewModels
{
    public class UserAdminViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Id { get; set; }

        public bool IsBanned { get; set; }

        public int TestDrivesCount { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserAdminViewModel>()
                .ForMember(dest => dest.TestDrivesCount, opt => opt.MapFrom(src => src.TestDrives.Count))
                .ForMember(dest => dest.IsBanned, opt => opt.MapFrom(src => src.LockoutEnd > DateTime.UtcNow));
        }
    }
}
