using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Models.UserModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IAdminUsersService
    {
        Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync(IUserSortStrategy sortStrategy);
        Task BanUserAsync(string userId);
        Task UnbanUserAsync(string userId);
        Task DeleteUserAsync(string userId);
    }
}
