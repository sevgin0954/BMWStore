using BMWStore.Models.SeriesModels.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISeriesService
    {
        Task CreateNewSeriesAsync(AdminSeriesCreateBindingModel model);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
    }
}
