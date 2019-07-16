using AutoMapper;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly IPicturesService picturesService;

        public AdminCarsService(IBMWStoreUnitOfWork unitOfWork, IPicturesService picturesService)
        {
            this.unitOfWork = unitOfWork;
            this.picturesService = picturesService;
        }

        public async Task CreateNewCar(AdminCarCreateBindingModel model)
        {
            var dbNewCar = Mapper.Map<NewCar>(model);
            var picturesByteData = await FileHelper.IFormFilesToByteAsync(model.Pictures);
            var pictures = this.picturesService.GetPicturesFromByteData(picturesByteData);
            dbNewCar.Pictures = pictures;

            this.unitOfWork.NewCars.Add(dbNewCar);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync()
        {
            var models = new List<CarConciseViewModel>();

            var usedCars = await this.unitOfWork.UsedCars.GetAllAsync();
            var newCars = await this.unitOfWork.NewCars.GetAllAsync();

            Mapper.Map(usedCars, models);
            Mapper.Map(newCars, models);

            return models;
        }
    }
}
