using AutoMapper;
using BMWStore.Data.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class NewCarsService : INewCarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public NewCarsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NewCarConciseViewModel>> GetAllAsync()
        {
            var dbCars = await this.unitOfWork.NewCars
                .GetAll()
                .ToArrayAsync();

            var carModels = Mapper.Map<IEnumerable<NewCarConciseViewModel>>(dbCars);

            return carModels;
        }
    }
}
