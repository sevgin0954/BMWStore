using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSeriesService
    {
        Task CreateNewAsync(SeriesServiceModel model);
        Task DeleteAsync(string seriesId);
        IQueryable<SeriesServiceModel> GetAll();
        Task<SeriesServiceModel> GetByIdAsync(string id);
        Task EditAsync(SeriesServiceModel model);
    }
}
