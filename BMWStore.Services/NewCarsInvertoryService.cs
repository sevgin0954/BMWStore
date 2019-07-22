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
    public class NewCarsInvertoryService : INewCarsInvertoryService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public NewCarsInvertoryService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CarViewModel>> GetAllAsync(ICarSortStrategy sortStrategy)
        {
            var carModels = await this.unitOfWork.NewCars
                .GetAllSorted(sortStrategy)
                .To<CarViewModel>()
                .ToArrayAsync();

            return carModels;
        }
    }
}
