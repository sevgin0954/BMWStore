using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCarsService
    {
        Task<AdminCarsViewModel> GetCarsViewModelAsync(
            string id,
            SortStrategyDirection sortDirection,
            AdminBaseCarSortStrategyType sortType,
            AdminBaseCarFilterStrategy filter,
            int pageNumber);
        Task CreateCarAsync<TCar>(AdminCarCreateBindingModel model) where TCar : BaseCar;
        Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model);
        Task EditCarAsync<TCar>(AdminCarEditBindingModel model) where TCar : BaseCar;
    }
}
