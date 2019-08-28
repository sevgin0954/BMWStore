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
        private readonly IAdminCommonDeleteService adminDeleteService;
        private readonly IAdminCommonEditService adminEditService;
        private readonly IAdminCommonCreateService adminCreateService;

        public AdminModelTypesService(
            IModelTypeRepository modelTypeRepository,
            IAdminCommonDeleteService adminDeleteService,
            IAdminCommonEditService adminEditService,
            IAdminCommonCreateService adminCreateService)
        {
            this.modelTypeRepository = modelTypeRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
            this.adminCreateService = adminCreateService;
        }

        public async Task CreateNewAsync(ModelTypeServiceModel model)
        {
            await adminCreateService.CreateAsync<ModelType, ModelTypeServiceModel>(model);
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
