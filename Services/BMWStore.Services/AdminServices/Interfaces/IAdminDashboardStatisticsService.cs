using BMWStore.Services.Models;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminDashboardStatisticsService
    {
        Task<DashboardStatisticsServiceModel> GetStatisticsAsync();
    }
}
