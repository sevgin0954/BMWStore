using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminUsersService
    {
        Task BanUserAsync(string userId);
        Task DeleteAsync(string userId);
        Task<IQueryable<UserServiceModel>> GetSortedUsersAsync(
            IUserSortStrategy sortStrategy,
            int pageNumber);
        Task<UserServiceModel> GetUserByIdAsync(string id);
        Task UnbanUserAsync(string userId);
    }
}
