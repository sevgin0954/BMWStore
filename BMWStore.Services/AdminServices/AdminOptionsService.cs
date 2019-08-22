using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
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
        private readonly IAdminEditService adminEditService;

        public AdminOptionsService(
            IOptionRepository optionRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            ISelectListItemsService selectListItemsService,
            IAdminEditService adminEditService)
        {
            this.optionRepository = optionRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.selectListItemsService = selectListItemsService;
            this.adminEditService = adminEditService;
        }

        public async Task<AdminOptionsViewModel> GetOptionsViewModelAsync(
            IOptionFilterStrategy filterStrategy,
            OptionSortStrategyType sortStrategyType,
            SortStrategyDirection sortDirection,
            int pageNumber)
        {
            var sortStrategy = OptionSortStrategyFactory.GetStrategy(sortStrategyType, sortDirection);

            var allOptions = this.optionRepository.GetAll();
            var filteredOptions = filterStrategy.Filter(allOptions);
            var sortedAndFilteredOptions = sortStrategy.Sort(filteredOptions);
            var optionModels = await this.readService.GetAllAsync<OptionViewModel, Option>(sortedAndFilteredOptions, pageNumber);

            var model = new AdminOptionsViewModel()
            {
                CurrentPage = pageNumber,
                Options = optionModels,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredOptions),
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortStrategyType
            };

            return model;
        }

        public async Task CreateNewOptionAsync(OptionBindingModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.optionRepository.Add(dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<OptionBindingModel> GetEditBindingModelAsync(string optionId)
        {
            var dbOption = await this.GetOptionAsync(optionId);

            var model = Mapper.Map<OptionBindingModel>(dbOption);

            var optionTypeModels = await this.selectListItemsService.GetAllAsSelectListItemsAsync<OptionType>();
            this.selectListItemsService.SelectItemsWithValues(optionTypeModels, model.OptionTypeId);
            model.OptionTypes = optionTypeModels;

            return model;
        }

        public async Task EditOptionAsync(OptionBindingModel model)
        {
            await this.adminEditService.EditAsync<Option, OptionBindingModel>(model, model.Id);
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
