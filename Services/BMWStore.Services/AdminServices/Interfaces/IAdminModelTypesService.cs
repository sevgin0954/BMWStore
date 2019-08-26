using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminModelTypesService
    {
        IQueryable<ModelTypeServiceModel> GetAll();
        Task CreateNewAsync(ModelTypeServiceModel model);
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;
        Task EditAsync(ModelTypeServiceModel model);
        Task DeleteAsync(string modelTypeId);
    }
}
