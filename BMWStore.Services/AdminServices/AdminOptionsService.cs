using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
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
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminOptionsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewOptionAsync(AdminOptionCreateBindingModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.unitOfWork.Options.Add(dbOption);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<OptionViewModel>> GetAllOptionsAsync()
        {
            var models = await this.unitOfWork.Options
                .GetAll()
                .To<OptionViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var models = await this.unitOfWork.Options
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return models;
        }

        public async Task DeleteOptionAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            this.unitOfWork.Options.Remove(dbOption);
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            var model = Mapper.Map<AdminCarOptionEditBindingModel>(dbOption);

            return model;
        }

        public async Task EditOption(AdminCarOptionEditBindingModel model)
        {
            var dbOption = await this.GetOptionAsync(model.Id);
            Mapper.Map(model, dbOption);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task<Option> GetOptionAsync(string optionId)
        {
            var dbOption = await this.unitOfWork.Options.GetByIdAsync(optionId);
            DataValidator.ValidateNotNull(dbOption, new ArgumentException(ErrorConstants.IncorrectId));

            return dbOption;
        }
    }
}
