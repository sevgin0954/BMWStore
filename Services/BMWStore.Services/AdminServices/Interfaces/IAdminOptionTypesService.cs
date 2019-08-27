using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task CreateNewAsync(OptionTypeServiceModel model);
        Task DeleteAsync(string optionTypeId);
        IQueryable<OptionTypeServiceModel> GetAll();
        Task<OptionTypeServiceModel> GetByIdAsync(string id);
        Task EditAsync(OptionTypeServiceModel model);
    }
}
