using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
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
    public class AdminEnginesService : IAdminEnginesService
    {
        private readonly IEngineRepository engineRepository;
        private readonly IAdminCommonDeleteService adminDeleteService;
        private readonly IAdminCommonEditService adminEditService;
        private readonly IAdminCommonCreateService adminCreateService;

        public AdminEnginesService(
            IEngineRepository engineRepository,
            IAdminCommonDeleteService adminDeleteService,
            IAdminCommonEditService adminEditService,
            IAdminCommonCreateService adminCreateService)
        {
            this.engineRepository = engineRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
            this.adminCreateService = adminCreateService;
        }

        public async Task CreateNewAsync(EngineServiceModel model)
        {
            await this.adminCreateService.CreateAsync<Engine, EngineServiceModel>(model);
        }

        public async Task DeleteAsync(string engineId)
        {
            await this.adminDeleteService.DeleteAsync<Engine>(engineId);
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

        public async Task<EngineServiceModel> GetByIdAsync(string id)
        {
            var model = await this.engineRepository
                .FindAll(id)
                .To<EngineServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(EngineServiceModel model)
        {
            await this.adminEditService.EditAsync<Engine, EngineServiceModel>(model, model.Id);
        }
    }
}
