using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
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

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(
            ICarSortStrategy<BaseCar> sortStrategy,
            ICarFilterStrategy filterStrategy)
        {
            var filteredCars = this.unitOfWork.AllCars.GetFiltered(filterStrategy);
            var sortedCars = sortStrategy.Sort(filteredCars);

            var models = await sortedCars
                .Include(uc => uc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();

            return models;
        }
    }
}
