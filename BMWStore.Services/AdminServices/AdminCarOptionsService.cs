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
    public class AdminCarOptionsService : IAdminCarOptionsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminCarOptionsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewCarOptionAsync(AdminOptionCreateBindingModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.unitOfWork.Options.Add(dbOption);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<OptionViewModel>> GetAllOptionsAsync()
        {
            var models = await this.unitOfWork.Options
                .GetAllAsQueryable()
                .To<OptionViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var models = await this.unitOfWork.Options
                .GetAllAsQueryable()
                .To<SelectListItem>()
                .ToArrayAsync();

            return models;
        }

        public async Task DeleteCarOptionAsync(string carOptionId)
        {
            var dbOption = await this.unitOfWork.Options.GetByIdAsync(carOptionId);
            DataValidator.ValidateNotNull(dbOption, new ArgumentException(ErrorConstants.IncorrectId));

            this.unitOfWork.Options.Remove(dbOption);
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
