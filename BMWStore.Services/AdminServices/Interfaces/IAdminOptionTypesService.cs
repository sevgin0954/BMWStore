using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task<IEnumerable<OptionTypeConciseViewModel>> GetAllAsync();
        Task CreateOptionTypeAsync(OptionTypeCreateBindingModel model);
        Task<OptionTypeEditBindingModel> GetEditingModelAsync(string optionTypeId);
        Task EditAsync(OptionTypeEditBindingModel model);
        Task DeleteAsync(string optionTypeId);
    }
}
