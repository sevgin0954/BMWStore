using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminCarsService : IAdminCarsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly IAdminPicturesService adminPicturesService;
        private readonly ISelectListItemsService selectListItemsService;

        public AdminCarsService(
            IBMWStoreUnitOfWork unitOfWork,
            IAdminPicturesService adminPicturesService,
            ISelectListItemsService selectListItemsService)
        {
            this.unitOfWork = unitOfWork;
            this.adminPicturesService = adminPicturesService;
            this.selectListItemsService = selectListItemsService;
        }

        public async Task CreateNewCar(AdminNewCarCreateBindingModel model)
        {
            var dbNewCar = Mapper.Map<NewCar>(model);
            await this.adminPicturesService.UpdateCarPicturesAsync(dbNewCar, model.Pictures);

            this.unitOfWork.NewCars.Add(dbNewCar);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task CreateUsedCar(AdminNewCarCreateBindingModel model)
        {
            var dbUsedCar = Mapper.Map<UsedCar>(model);
            await this.adminPicturesService.UpdateCarPicturesAsync(dbUsedCar, model.Pictures);

            this.unitOfWork.UsedCars.Add(dbUsedCar);

            // TODO: Repeating code
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task DeleteCarAsync(string carId)
        {
            var dbCar = await this.unitOfWork.AllCars.GetByIdAsync(carId);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            this.unitOfWork.AllCars.Remove(dbCar);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model)
        {
            var allOptions = model.CarOptions;
            var carId = model.Id;
            var carModel = await this.unitOfWork.AllCars
                    .GetAll()
                    .Where(c => c.Id == carId)
                    .To<AdminCarEditBindingModel>()
                    .FirstAsync();

            Mapper.Map(carModel, model);
            var selectedOptions = carModel.CarOptions;

            this.selectListItemsService.SelectItemsWithValues(model.Engines, model.SelectedEngineId);
            this.selectListItemsService.SelectItemsWithValues(model.FuelTypes, model.SelectedFuelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.ModelTypes, model.SelectedModelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.Series, model.SelectedSeriesId);
            this.selectListItemsService.SelectItemsWithValues(allOptions, selectedOptions.Select(o => o.Value).ToArray());

            model.CarOptions = allOptions;
        }

        public async Task EditNewCarAsync(AdminCarEditBindingModel model)
        {
            var dbCar = await this.unitOfWork.NewCars.GetByIdAsync(model.Id);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(model, dbCar);
            // TODO: REAPETING CODE
            if (model.Pictures.Count() > 0)
            {
                await this.adminPicturesService.UpdateCarPicturesAsync(dbCar, model.Pictures);
            }

            if (model.CarOptions.Count() > 0)
            {
                await this.unitOfWork.CarsOptions.RemoveWithCarIdAsync(model.Id);
            }

            await this.unitOfWork.CompleteAsync();
        }

        public async Task EditUsedCarAsync(AdminCarEditBindingModel model)
        {
            var dbCar = await this.unitOfWork.UsedCars.GetByIdAsync(model.Id);
            DataValidator.ValidateNotNull(dbCar, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(model, dbCar);
            // TODO: REAPETING CODE
            if (model.Pictures.Count() > 0)
            {
                await this.adminPicturesService.UpdateCarPicturesAsync(dbCar, model.Pictures);
            }

            if (model.CarOptions.Count() > 0)
            {
                await this.unitOfWork.CarsOptions.RemoveWithCarIdAsync(model.Id);
            }

            await this.unitOfWork.CompleteAsync();
        }
    }
}
