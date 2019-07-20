using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly ICloudinaryService cloudinaryService;

        public AdminCarsService(
            IBMWStoreUnitOfWork unitOfWork, 
            ICloudinaryService cloudinaryService)
        {
            this.unitOfWork = unitOfWork;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateNewCar(AdminNewCarCreateBindingModel model)
        {
            var dbNewCar = Mapper.Map<NewCar>(model);
            await this.AddPicturesToCar(dbNewCar, model.Pictures);

            this.unitOfWork.NewCars.Add(dbNewCar);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task CreateUsedCar(AdminNewCarCreateBindingModel model)
        {
            var dbUsedCar = Mapper.Map<NewCar>(model);
            await this.AddPicturesToCar(dbUsedCar, model.Pictures);

            this.unitOfWork.NewCars.Add(dbUsedCar);

            // TODO: Repeating code
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task AddPicturesToCar(BaseCar car, IEnumerable<IFormFile> pictures)
        {
            var pictureUrls = await this.cloudinaryService.UploadPicturesAsync(pictures);
            car.Pictures = Mapper.Map<ICollection<Picture>>(pictureUrls);
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync()
        {
            var models = new List<CarConciseViewModel>();

            var usedCarsModels = await this.unitOfWork.UsedCars
                .GetAllAsQueryable()
                .Include(uc => uc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();
            var newCarsModels = await this.unitOfWork.NewCars
                .GetAllAsQueryable()
                .Include(nc => nc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();

            models.AddRange(usedCarsModels);
            models.AddRange(newCarsModels);

            return models;
        }

        public async Task DeleteCarAsync(string carId)
        {
            var dbCar = await this.unitOfWork.AllCars.GetByIdAsync(carId);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            this.unitOfWork.AllCars.Remove(dbCar);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public Task GetEditBindingModel(string id)
        {
            throw new NotImplementedException();
        }
    }
}
