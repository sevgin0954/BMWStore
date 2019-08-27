using AutoMapper;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Models.UserModels.ViewModels
{
    public class UserAdminViewModel : IMapFrom<UserServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public bool IsBanned { get; set; }

        public int TestDrivesCount { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserServiceModel, UserAdminViewModel>()
                .ForMember(dest => dest.TestDrivesCount, opt => opt.MapFrom(src => src.TestDrivesCount))
                .ForMember(dest => dest.IsBanned, opt => opt.MapFrom(src => src.LockoutEnd > DateTime.UtcNow));
        }
    }
}
