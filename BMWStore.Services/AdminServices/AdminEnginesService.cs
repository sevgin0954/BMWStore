using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminEnginesService : IAdminEnginesService
    {
        private readonly IEngineRepository engineRepository;
        private readonly ISelectListItemsService selectListItemsService;

        public AdminEnginesService(IEngineRepository engineRepository, ISelectListItemsService selectListItemsService)
        {
            this.engineRepository = engineRepository;
            this.selectListItemsService = selectListItemsService;
        }

        public async Task CreateEngineAsync(AdminEngineCreateBindingModel model)
        {
            var dbEngine = Mapper.Map<Engine>(model);
            this.engineRepository.Add(dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<EngineViewModel>> GetAllAsync()
        {
            var models = await this.engineRepository
                .GetAll()
                .To<EngineViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.engineRepository
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
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

        public async Task DeleteAsync(string engineId)
        {
            var dbEngine = await this.GetDbEngineAsync(engineId);
            this.engineRepository.Remove(dbEngine);

            var rowsAffected = await this.engineRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task<Engine> GetDbEngineAsync(string engineId)
        {
            var dbEngine = await this.engineRepository.GetByIdAsync(engineId);
            DataValidator.ValidateNotNull(dbEngine, new ArgumentException(ErrorConstants.IncorrectId));

            return dbEngine;
        }
    }
}
