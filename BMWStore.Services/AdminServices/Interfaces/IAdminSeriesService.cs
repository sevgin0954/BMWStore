using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSeriesService
    {
        Task CreateNewSeriesAsync(SeriesCreateBindingModel model);
        Task<IEnumerable<SeriesViewModel>> GetAllAsync();
        Task<SeriesEditBindingModel> GetEditingModelAsync(string seriesId);
        Task EditAsync(SeriesEditBindingModel model);
        Task DeleteAsync(string seriesId);
    }
}
