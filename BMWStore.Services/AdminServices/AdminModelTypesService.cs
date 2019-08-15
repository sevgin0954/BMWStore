using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminModelTypesService : IAdminModelTypesService
    {
        private readonly IModelTypeRepository modelTypeRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminModelTypesService(
            IModelTypeRepository modelTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService)
        {
            this.modelTypeRepository = modelTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task<IEnumerable<ModelTypeViewModel>> GetAllAsync()
        {
            var models = await this.readService.GetAllAsync<ModelTypeViewModel, ModelType>();

            return models;
        }

        public async Task CreateNewModelType(AdminModelTypeCreateBidningModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.modelTypeRepository.Add(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string modelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<ModelType>(modelTypeId);
        }
    }
}
