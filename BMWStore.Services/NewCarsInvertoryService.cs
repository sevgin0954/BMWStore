using AutoMapper;
using BMWStore.Data.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
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

        public async Task<IEnumerable<NewCarConciseViewModel>> GetAllAsync()
        {
            var dbCars = await this.unitOfWork.NewCars
                .GetAllAsync();

            var carModels = Mapper.Map<IEnumerable<NewCarConciseViewModel>>(dbCars);

            return carModels;
        }
    }
}
