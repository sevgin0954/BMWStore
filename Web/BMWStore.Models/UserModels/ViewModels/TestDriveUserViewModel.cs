using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.UserModels.ViewModels
{
    public class TestDriveUserViewModel : IMapFrom<UserServiceModel>
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
