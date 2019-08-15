using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminModelTypesService
    {
        Task<IEnumerable<ModelTypeViewModel>> GetAllAsync();
        Task CreateNewModelType(AdminModelTypeCreateBidningModel model);
        Task DeleteAsync(string modelTypeId);
    }
}
