using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task<AdminCarsViewModel> GetCarsViewModelAsync(
            ICarFilterStrategy filterStrategy,
            SortStrategyDirection sortDirection,
            AdminBaseCarSortStrategyType sortType,
            int pageNumber);
        Task CreateCarAsync<TCar>(AdminCarBindingModel model) where TCar : BaseCar;
        Task SetEditBindingModelPropertiesAsync(AdminCarBindingModel model);
        Task EditCarAsync<TCar>(AdminCarBindingModel model) where TCar : BaseCar;
        Task DeleteAsync(string carId);
    }
}
