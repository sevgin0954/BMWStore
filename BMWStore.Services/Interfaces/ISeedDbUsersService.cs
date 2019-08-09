using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbUsersService
    {
        Task SeedUserAsync(string password, string email, string roleName);
    }
}
