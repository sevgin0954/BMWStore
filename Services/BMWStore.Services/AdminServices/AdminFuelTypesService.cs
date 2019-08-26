using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminFuelTypesService : IAdminFuelTypesService
    {
        private readonly IFuelTypeRepository fuelTypeRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;
        private readonly IReadService readService;

        public AdminFuelTypesService(
            IFuelTypeRepository fuelTypeRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService,
            IReadService readService)
        {
            this.fuelTypeRepository = fuelTypeRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
            this.readService = readService;
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
        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, FuelType>(id);

            return model;
        }

        public async Task CreateNewAsync(FuelTypeServiceModel model)
        {
            var dbFuelType = Mapper.Map<FuelType>(model);
            this.fuelTypeRepository.Add(dbFuelType);

            var rowsAffected = await this.fuelTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task EditAsync(FuelTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<FuelType, FuelTypeServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string fuelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<FuelType>(fuelTypeId);
        }
    }
}