using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Models.FuelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminFuelTypesService : IAdminFuelTypesService
    {
        private readonly IFuelTypeRepository fuelTypeRepository;

        public AdminFuelTypesService(IFuelTypeRepository fuelTypeRepository)
        {
            this.fuelTypeRepository = fuelTypeRepository;
        }

        public async Task CreateNewFuelTypeAsync(AdminFuelTypeCreateBindingModel model)
        {
            var dbFuelType = Mapper.Map<FuelType>(model);
            this.fuelTypeRepository.Add(dbFuelType);

            var rowsAffected = await this.fuelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<FuelTypeViewModel>> GetAllAsync()
        {
            var models = await this.fuelTypeRepository
                .GetAll()
                .To<FuelTypeViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.fuelTypeRepository
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }

        public async Task DeleteAsync(string fuelTypeId)
        {
            var dbFuelType = await this.fuelTypeRepository.GetByIdAsync(fuelTypeId);
            DataValidator.ValidateNotNull(dbFuelType, new ArgumentException(ErrorConstants.IncorrectId));

            this.fuelTypeRepository.Remove(dbFuelType);

            var rowsAffected = await this.fuelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
