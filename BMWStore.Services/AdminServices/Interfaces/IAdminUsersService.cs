using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.UserModels.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminUsersService
    {
        Task<UserAdminViewModel> GetUserByIdAsync(string id);
        Task<AdminUsersViewModel> GetSortedUsersAsync(
            IRequestCookieCollection requestCookies,
            int pageNumber);
        Task BanUserAsync(string userId);
        Task UnbanUserAsync(string userId);
        Task DeleteUserAsync(string userId);
    }
}
