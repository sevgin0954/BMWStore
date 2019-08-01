using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionsService : IAdminOptionsService
    {
        private readonly IOptionRepository optionRepository;

        public AdminOptionsService(IOptionRepository optionRepository)
        {
            this.optionRepository = optionRepository;
        }

        public async Task CreateNewOptionAsync(AdminOptionCreateBindingModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.optionRepository.Add(dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<OptionViewModel>> GetAllOptionsAsync()
        {
            var models = await this.optionRepository
                .GetAll()
                .To<OptionViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var models = await this.optionRepository
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return models;
        }

        public async Task DeleteAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            this.optionRepository.Remove(dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            var model = Mapper.Map<AdminCarOptionEditBindingModel>(dbOption);

            return model;
        }

        public async Task EditOptionAsync(AdminCarOptionEditBindingModel model)
        {
            var dbOption = await this.GetOptionAsync(model.Id);
            DataValidator.ValidateNotNull(dbOption, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(model, dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task<Option> GetOptionAsync(string optionId)
        {
            var dbOption = await this.optionRepository.GetByIdAsync(optionId);
            DataValidator.ValidateNotNull(dbOption, new ArgumentException(ErrorConstants.IncorrectId));

            return dbOption;
        }
    }
}
