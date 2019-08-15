using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminModelTypesService : IAdminModelTypesService
    {
        private readonly IModelTypeRepository modelTypeRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminModelTypesService(
            IModelTypeRepository modelTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.modelTypeRepository = modelTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task<IEnumerable<ModelTypeViewModel>> GetAllAsync()
        {
            var models = await this.readService.GetAllAsync<ModelTypeViewModel, ModelType>();

            return models;
        }

        public async Task CreateNewModelType(ModelTypeCreateBidningModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.modelTypeRepository.Add(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<ModelTypeEditBindingModel> GetEditingModel(string modelTypeId)
        {
            var model = await this.readService.GetModelByIdAsync<ModelTypeEditBindingModel, ModelType>(modelTypeId);

            return model;
        }

        public async Task EditAsync(ModelTypeEditBindingModel model)
        {
            await this.adminEditService.EditAsync<ModelType, ModelTypeEditBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string modelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<ModelType>(modelTypeId);
        }
    }
}
