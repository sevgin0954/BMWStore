using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task<IEnumerable<OptionTypeViewModel>> GetAllAsync();
        Task CreateOptionTypeAsync(OptionTypeCreateBindingModel model);
        Task DeleteAsync(string optionTypeId);
    }
}
