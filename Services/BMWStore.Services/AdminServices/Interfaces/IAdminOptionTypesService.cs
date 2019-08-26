using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionTypesService
    {
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;
        IQueryable<OptionTypeServiceModel> GetAll();
        Task CreateNewAsync(OptionTypeServiceModel model);
        Task EditAsync(OptionTypeServiceModel model);
        Task DeleteAsync(string optionTypeId);
    }
}
