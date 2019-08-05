using BMWStore.Models.AdminModels.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminUsersService
    {
        Task<AdminUsersViewModel> GetSortedUsersAsync(IRequestCookieCollection requestCookies, int pageNumber);
        Task BanUserAsync(string userId);
        Task UnbanUserAsync(string userId);
        Task DeleteUserAsync(string userId);
    }
}
