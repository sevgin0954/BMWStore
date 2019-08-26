using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.UserModels.ViewModels
{
    public class TestDriveUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
