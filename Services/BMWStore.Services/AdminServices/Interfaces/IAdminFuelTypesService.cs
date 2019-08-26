using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        IQueryable<FuelTypeServiceModel> GetAll();
        IQueryable<FuelTypeServiceModel> GetAll(int pageNumber);
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;
        Task CreateNewAsync(FuelTypeServiceModel model);
        Task EditAsync(FuelTypeServiceModel model);
        Task DeleteAsync(string fuelTypeId);
    }
}
