using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Models.FuelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
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
    }
}
