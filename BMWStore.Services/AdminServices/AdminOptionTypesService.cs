﻿using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionTypesService : IAdminOptionTypesService
    {
        private readonly IOptionTypeRepository optionTypeRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminOptionTypesService(
            IOptionTypeRepository optionTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService)
        {
            this.optionTypeRepository = optionTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task<IEnumerable<OptionTypeViewModel>> GetAllAsync()
        {
            var models = await this.readService.GetAllAsync<OptionTypeViewModel, OptionType>();

            return models;
        }

        public async Task CreateOptionTypeAsync(OptionTypeCreateBindingModel model)
        {
            var dbOptionType = Mapper.Map<OptionType>(model);
            this.optionTypeRepository.Add(dbOptionType);

            var rowsAffected = await this.optionTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string optionTypeId)
        {
            await this.adminDeleteService.DeleteAsync<OptionType>(optionTypeId);
        }
    }
}