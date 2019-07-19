using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminDashboardStatisticsService
    {
        Task<AdminDashboardStatisticsViewModel> GetStatisticsAsync();
    }
}
