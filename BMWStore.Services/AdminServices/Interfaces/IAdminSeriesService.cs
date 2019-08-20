using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSeriesService
    {
        Task CreateNewSeriesAsync(SeriesBindingModel model);
        Task<IEnumerable<SeriesViewModel>> GetAllAsync();
        Task<SeriesBindingModel> GetEditingModelAsync(string seriesId);
        Task EditAsync(SeriesBindingModel model);
        Task DeleteAsync(string seriesId);
    }
}
