using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminUsersService
    {
        Task<TModel> GetUserByIdAsync<TModel>(string id) where TModel : class;
        Task<IQueryable<UserServiceModel>> GetSortedUsersAsync(
            IUserSortStrategy sortStrategy,
            int pageNumber);
        Task BanUserAsync(string userId);
        Task UnbanUserAsync(string userId);
        Task DeleteAsync(string userId);
    }
}
