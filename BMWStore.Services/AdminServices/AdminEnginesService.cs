﻿using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
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
        private readonly IAdminEditService adminEditService;

        public AdminEnginesService(
            IEngineRepository engineRepository, 
            ISelectListItemsService selectListItemsService,
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.engineRepository = engineRepository;
            this.selectListItemsService = selectListItemsService;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateEngineAsync(EngineBindingModel model)
        {
            var dbEngine = Mapper.Map<Engine>(model);
            this.engineRepository.Add(dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminEnginesViewModel> GetEnginesViewModelAsync(
            int pageNumber, 
            IEngineSortStrategy engineSortStrategy)
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

        public async Task<EngineBindingModel> GetEditModelAsync(string engineId)
        {
            var model = await this.readService.GetModelByIdAsync<EngineBindingModel, Engine>(engineId);

            var allTransmissions = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Transmission>();

            var selectedTransmissionsIds = model.Transmissions.Select(t => t.Value).ToArray();
            this.selectListItemsService.SelectItemsWithValues(allTransmissions, selectedTransmissionsIds);

            model.Transmissions = allTransmissions;

            return model;
        }

        public async Task EditAsync(EngineBindingModel model)
        {
            await this.adminEditService.EditAsync<Engine, EngineBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string engineId)
        {
            await this.adminDeleteService.DeleteAsync<Engine>(engineId);
        }
    }
}
