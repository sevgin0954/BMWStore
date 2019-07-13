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
        private readonly IMapper mapper;
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public NewCarsInvertoryService(IBMWStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NewCarConciseViewModel>> GetAllAsync()
        {
            var dbCars = await this.unitOfWork.NewCars
                .GetAllAsync();

            var carModels = this.mapper.Map<IEnumerable<NewCarConciseViewModel>>(dbCars);

            return carModels;
        }
    }
}
