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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var dbUsedCar = Mapper.Map<NewCar>(model);
            await this.adminPicturesService.UpdateCarPicturesAsync(dbUsedCar, model.Pictures);

            this.unitOfWork.NewCars.Add(dbUsedCar);

            // TODO: Repeating code
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync()
        {
            var models = new List<CarConciseViewModel>();

            var usedCarsModels = await this.unitOfWork.UsedCars
                .GetAll()
                .Include(uc => uc.Pictures)
                .To<CarConciseViewModel>()
                .ToArrayAsync();
            var newCarsModels = await this.unitOfWork.NewCars
                .GetAll()
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

        public async Task SetEditBindingModelPropertiesAsync(AdminCarEditBindingModel model)
        {
            var allOptions = model.CarOptions;
            // TODO: REAPEATING CODE AND THIS IS USING TWO QUERIES FOR ONE THING
            var carId = model.Id;
            var carModel = new AdminCarEditBindingModel();
            if (await this.IsNewCar(carId))
            {
                carModel = await this.unitOfWork.NewCars
                    .GetAll()
                    .Where(c => c.Id == carId)
                    .To<AdminCarEditBindingModel>()
                    .FirstAsync();
                carModel.IsNew = true;
            }
            else
            {
                carModel = await this.unitOfWork.UsedCars
                    .GetAll()
                    .Where(c => c.Id == carId)
                    .To<AdminCarEditBindingModel>()
                    .FirstAsync();
                carModel.IsNew = false;
            }

            Mapper.Map(carModel, model);
            var selectedOptions = carModel.CarOptions;

            this.selectListItemsService.SelectItemsWithValues(model.Engines, model.SelectedEngineId);
            this.selectListItemsService.SelectItemsWithValues(model.FuelTypes, model.SelectedFuelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.ModelTypes, model.SelectedModelTypeId);
            this.selectListItemsService.SelectItemsWithValues(model.Series, model.SelectedSeriesId);
            this.selectListItemsService.SelectItemsWithValues(allOptions, selectedOptions.Select(o => o.Value).ToArray());

            model.CarOptions = allOptions;
        }

        private async Task<bool> IsNewCar(string carId)
        {
            var isNewCar = await this.unitOfWork.AllCars.IsType(typeof(NewCar), carId);

            return isNewCar;
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
