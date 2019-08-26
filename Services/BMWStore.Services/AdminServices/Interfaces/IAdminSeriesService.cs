using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSeriesService
    {
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;
        Task CreateNewAsync(SeriesServiceModel model);
        IQueryable<SeriesServiceModel> GetAll();
        Task EditAsync(SeriesServiceModel model);
        Task DeleteAsync(string seriesId);
    }
}
