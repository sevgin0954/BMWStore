using AutoMapper;
using BMWStore.Data.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdminCarsService(IBMWStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync()
        {
            var models = new List<CarConciseViewModel>();

            var usedCars = await this.unitOfWork.UsedCars.GetAllAsync();
            var newCars = await this.unitOfWork.NewCars.GetAllAsync();

            this.mapper.Map(usedCars, models);
            this.mapper.Map(newCars, models);

            return models;
        }
    }
}
