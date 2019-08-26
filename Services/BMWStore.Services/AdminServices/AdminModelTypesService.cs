using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using System.Linq;
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

        public IQueryable<ModelTypeServiceModel> GetAll()
        {
            var models = this.modelTypeRepository.GetAll().To<ModelTypeServiceModel>();

            return models;
        }

        public async Task CreateNewAsync(ModelTypeServiceModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.modelTypeRepository.Add(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, ModelType>(id);

            return model;
        }

        public async Task EditAsync(ModelTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<ModelType, ModelTypeServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string modelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<ModelType>(modelTypeId);
        }
    }
}
