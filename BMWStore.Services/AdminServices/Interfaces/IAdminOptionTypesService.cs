using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task<IEnumerable<OptionTypeConciseViewModel>> GetAllAsync();
        Task CreateOptionTypeAsync(OptionTypeBindingModel model);
        Task<OptionTypeBindingModel> GetEditingModelAsync(string optionTypeId);
        Task EditAsync(OptionTypeBindingModel model);
        Task DeleteAsync(string optionTypeId);
    }
}
