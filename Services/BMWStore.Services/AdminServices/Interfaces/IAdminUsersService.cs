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
        Task<IQueryable<UserConciseServiceModel>> GetSortedUsersAsync(
            IUserSortStrategy sortStrategy,
            int pageNumber);
        Task<UserConciseServiceModel> GetUserByIdAsync(string id);
        Task UnbanUserAsync(string userId);
    }
}
