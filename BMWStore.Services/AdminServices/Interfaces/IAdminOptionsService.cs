using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Models.OptionModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionsService
    {
        Task CreateNewOptionAsync(AdminOptionCreateBindingModel model);
        Task<IEnumerable<OptionViewModel>> GetAllOptionsAsync();
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync();
        Task DeleteOptionAsync(string carOptionId);
        Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string carOptionId);
        Task EditOption(AdminCarOptionEditBindingModel model);
    }
}
