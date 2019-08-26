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
    public class AdminOptionTypesService : IAdminOptionTypesService
    {
        private readonly IOptionTypeRepository optionTypeRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminOptionTypesService(
            IOptionTypeRepository optionTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.optionTypeRepository = optionTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, OptionType>(id);

            return model;
        }

        public IQueryable<OptionTypeServiceModel> GetAll()
        {
            var models = this.optionTypeRepository.GetAll().To<OptionTypeServiceModel>();

            return models;
        }

        public async Task CreateNewAsync(OptionTypeServiceModel model)
        {
            var dbOptionType = Mapper.Map<OptionType>(model);
            this.optionTypeRepository.Add(dbOptionType);

            var rowsAffected = await this.optionTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task EditAsync(OptionTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<OptionType, OptionTypeServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string optionTypeId)
        {
            await this.adminDeleteService.DeleteAsync<OptionType>(optionTypeId);
        }
    }
}