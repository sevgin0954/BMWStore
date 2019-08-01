using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminSeriesService
    {
        Task CreateNewSeriesAsync(AdminSeriesCreateBindingModel model);
        Task<IEnumerable<SeriesViewModel>> GetAllAsync();
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task DeleteAsync(string seriesId);
    }
}
