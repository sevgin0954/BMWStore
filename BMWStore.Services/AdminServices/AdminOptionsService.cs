using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionsService : IAdminOptionsService
    {
        private readonly IOptionRepository optionRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly ISelectListItemsService selectListItemsService;

        public AdminOptionsService(
            IOptionRepository optionRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            ISelectListItemsService selectListItemsService)
        {
            this.optionRepository = optionRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.selectListItemsService = selectListItemsService;
        }

        public async Task<AdminOptionsViewModel> GetOptionsViewModelAsync(IOptionFilterStrategy filterStrategy, int pageNumber)
        {
            var allOptions = this.optionRepository.GetAll();
            var filteredOptions = filterStrategy.Filter(allOptions);
            var optionModels = await this.readService.GetAllAsync<OptionViewModel, Option>(filteredOptions, pageNumber);

            var model = new AdminOptionsViewModel()
            {
                CurrentPage = pageNumber,
                Options = optionModels,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(allOptions)
            };

            return model;
        }

        public async Task CreateNewOptionAsync(AdminOptionCreateBindingModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.optionRepository.Add(dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            var model = Mapper.Map<AdminCarOptionEditBindingModel>(dbOption);

            var optionTypeModels = await this.selectListItemsService.GetAllAsSelectListItemsAsync<OptionType>();
            this.selectListItemsService.SelectItemsWithValues(optionTypeModels, model.OptionTypeId);
            model.OptionTypes = optionTypeModels;

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

        public async Task DeleteAsync(string optionId)
        {
            await this.adminDeleteService.DeleteAsync<Option>(optionId);
        }
    }
}
