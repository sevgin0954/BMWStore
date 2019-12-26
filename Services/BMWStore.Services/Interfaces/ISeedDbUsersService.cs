using BMWStore.Services.Models;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbUsersService
    {
        Task SeedUserAsync(UserServiceModel model, string password, string roleName);
    }
}
