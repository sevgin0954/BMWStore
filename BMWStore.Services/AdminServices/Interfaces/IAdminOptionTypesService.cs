using BMWStore.Models.OptionTypeModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task CreateOptionTypeAsync(OptionTypeCreateBindingModel model);
    }
}
