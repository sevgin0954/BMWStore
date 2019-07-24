using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsService : ICarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public CarsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(ICarSortStrategy sortStrategy)
        {
            var models = await this.unitOfWork.AllCars
                .GetAllSorted(sortStrategy)
                .Include(uc => uc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllNewCarsAsync(
            ICarSortStrategy sortStrategy, 
            params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.unitOfWork.NewCars
                .GetFiltered(filterStrategies);
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);
            var models = await sortedAndFilteredCars
                .To<CarConciseViewModel>()
                .ToArrayAsync();

            return models;
        }
    }
}
