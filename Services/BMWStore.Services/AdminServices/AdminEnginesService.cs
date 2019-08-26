using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
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
    public class AdminEnginesService : IAdminEnginesService
    {
        private readonly IEngineRepository engineRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;
        private readonly IReadService readService;

        public AdminEnginesService(
            IEngineRepository engineRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService,
            IReadService readService)
        {
            this.engineRepository = engineRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
            this.readService = readService;
        }

        public IQueryable<EngineServiceModel> GetAll()
        {
            var models = this.engineRepository.GetAll().To<EngineServiceModel>();

            return models;
        }

        public IQueryable<EngineServiceModel> GetSorted(IEngineSortStrategy sortStrategy, int pageNumber)
        {
            var sortedEngines = sortStrategy.Sort(this.engineRepository.GetAll());
            var enginesFromPage = sortedEngines.GetFromPage(pageNumber);
            var engineModels = enginesFromPage.To<EngineServiceModel>();

            return engineModels;
        }

        public async Task CreateNewAsync(EngineServiceModel model)
        {
            var dbEngine = Mapper.Map<Engine>(model);
            this.engineRepository.Add(dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task<TModel> GetEngineByIdAsync<TModel>(string id)
            where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, Engine>(id);

            return model;
        }

        public async Task EditAsync(EngineServiceModel model)
        {
            await this.adminEditService.EditAsync<Engine, EngineServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string engineId)
        {
            await this.adminDeleteService.DeleteAsync<Engine>(engineId);
        }
    }
}
