using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminFuelTypesService
    {
        Task CreateNewAsync(FuelTypeServiceModel model);
        Task DeleteAsync(string fuelTypeId);
        IQueryable<FuelTypeServiceModel> GetAll();
        IQueryable<FuelTypeServiceModel> GetAll(int pageNumber);
        Task<FuelTypeServiceModel> GetByIdAsync(string id);
        Task EditAsync(FuelTypeServiceModel model);
    }
}