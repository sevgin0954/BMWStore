using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminEnginesService : IAdminEnginesService
    {
        private readonly IEngineRepository engineRepository;
        private readonly ISelectListItemsService selectListItemsService;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminEnginesService(
            IEngineRepository engineRepository, 
            ISelectListItemsService selectListItemsService,
            IReadService readService,
            IAdminDeleteService adminDeleteService)
        {
            this.engineRepository = engineRepository;
            this.selectListItemsService = selectListItemsService;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task CreateEngineAsync(AdminEngineCreateBindingModel model)
        {
            var dbEngine = Mapper.Map<Engine>(model);
            this.engineRepository.Add(dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminEnginesViewModel> GetEnginesViewModelAsync(int pageNumber, IEngineSortStrategy engineSortStrategy)
        {
            var sortedEngines = engineSortStrategy.Sort(this.engineRepository.GetAll());
            var engineModels = await this.readService.GetAllAsync<EngineViewModel, Engine>(sortedEngines, pageNumber);

            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(this.engineRepository.GetAll());
            var model = new AdminEnginesViewModel()
            {
                Engines = engineModels,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return model;
        }

        public async Task SetEditBindingModelPropertiesAsync(AdminEngineEditBindingModel model)
        {
            var allTransmissions = model.Transmissions;

            var dbEngine = await this.GetDbEngineAsync(model.Id);
            Mapper.Map(dbEngine, model);

            var selectedTransmissionsIds = model.Transmissions.Select(t => t.Value).ToArray();
            this.selectListItemsService.SelectItemsWithValues(allTransmissions, selectedTransmissionsIds);

            model.Transmissions = allTransmissions;
        }

        public async Task EditAsync(AdminEngineEditBindingModel model)
        {
            var dbEngine = await this.GetDbEngineAsync(model.Id);
            Mapper.Map(model, dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task<Engine> GetDbEngineAsync(string engineId)
        {
            var dbEngine = await this.engineRepository.GetByIdAsync(engineId);
            DataValidator.ValidateNotNull(dbEngine, new ArgumentException(ErrorConstants.IncorrectId));

            return dbEngine;
        }

        public async Task DeleteAsync(string engineId)
        {
            await this.adminDeleteService.DeleteAsync<Engine>(engineId);
        }
    }
}
