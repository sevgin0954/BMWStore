using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminModelTypesService
    {
        Task CreateNewAsync(ModelTypeServiceModel model);
        Task DeleteAsync(string modelTypeId);
        IQueryable<ModelTypeServiceModel> GetAll();
        Task<ModelTypeServiceModel> GetByIdAsync(string id);
        Task EditAsync(ModelTypeServiceModel model);
    }
}
