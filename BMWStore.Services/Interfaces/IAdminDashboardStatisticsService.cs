using BMWStore.Models.AdminModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IAdminDashboardStatisticsService
    {
        Task<AdminDashboardStatisticsViewModel> GetStatisticsAsync();
    }
}
