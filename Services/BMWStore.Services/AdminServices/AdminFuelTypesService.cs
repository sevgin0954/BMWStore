using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminFuelTypesService : IAdminFuelTypesService
    {
        private readonly IFuelTypeRepository fuelTypeRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminFuelTypesService(
            IFuelTypeRepository fuelTypeRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.fuelTypeRepository = fuelTypeRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateNewAsync(FuelTypeServiceModel model)
        {
            var dbFuelType = Mapper.Map<FuelType>(model);
            this.fuelTypeRepository.Add(dbFuelType);

            var rowsAffected = await this.fuelTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string fuelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<FuelType>(fuelTypeId);
        }

        public IQueryable<FuelTypeServiceModel> GetAll()
        {
            var models = this.fuelTypeRepository
                .GetAll()
                .To<FuelTypeServiceModel>();

            return models;
        }

        public IQueryable<FuelTypeServiceModel> GetAll(int pageNumber)
        {
            var models = this.fuelTypeRepository
                .GetAll()
                .GetFromPage(pageNumber)
                .To<FuelTypeServiceModel>();

            return models;
        }

        public async Task<FuelTypeServiceModel> GetByIdAsync(string id)
        {
            var model = await this.fuelTypeRepository
                .FindAll(id)
                .To<FuelTypeServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(FuelTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<FuelType, FuelTypeServiceModel>(model, model.Id);
        }
    }
}