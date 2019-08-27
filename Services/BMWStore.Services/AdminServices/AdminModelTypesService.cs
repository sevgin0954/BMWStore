using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminModelTypesService : IAdminModelTypesService
    {
        private readonly IModelTypeRepository modelTypeRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminModelTypesService(
            IModelTypeRepository modelTypeRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.modelTypeRepository = modelTypeRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateNewAsync(ModelTypeServiceModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.modelTypeRepository.Add(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string modelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<ModelType>(modelTypeId);
        }

        public IQueryable<ModelTypeServiceModel> GetAll()
        {
            var models = this.modelTypeRepository.GetAll().To<ModelTypeServiceModel>();

            return models;
        }

        public async Task<ModelTypeServiceModel> GetByIdAsync(string id)
        {
            var model = await this.modelTypeRepository
                .FindAll(id)
                .To<ModelTypeServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(ModelTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<ModelType, ModelTypeServiceModel>(model, model.Id);
        }
    }
}
