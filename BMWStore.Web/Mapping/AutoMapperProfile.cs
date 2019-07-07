using AutoMapper;
using BMWStore.Entities;
using BMWStore.Models.UserModels.ViewModels;
using System;

namespace BMWStore.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserAdminViewModel>()
                .ForMember(dest => dest.IsBanned, opt => opt.MapFrom(src => src.LockoutEnd > DateTime.UtcNow))
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.Orders.Count))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
