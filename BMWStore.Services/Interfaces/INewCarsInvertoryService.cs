using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface INewCarsInvertoryService
    {
        Task<NewCarsInvertoryViewModel> GetInvertoryBindingModel(
            ICarSortStrategy sortStrategy, 
            params ICarFilterStrategy[] filterStrategiesparams);
    }
}
