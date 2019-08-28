using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.OptionStrategies.Interfaces;
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
    public class AdminOptionsService : IAdminOptionsService
    {
        private readonly IOptionRepository optionRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;
        private readonly IAdminCreateService adminCreateService;

        public AdminOptionsService(
            IOptionRepository optionRepository, 
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService,
            IAdminCreateService adminCreateService)
        {
            this.optionRepository = optionRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
            this.adminCreateService = adminCreateService;
        }

        public async Task CreateNewAsync(OptionServiceModel model)
        {
            await this.adminCreateService.CreateAsync<Option, OptionServiceModel>(model);
        }

        public async Task DeleteAsync(string optionId)
        {
            await this.adminDeleteService.DeleteAsync<Option>(optionId);
        }

        public IQueryable<OptionServiceModel> GetAll()
        {
            var optionModels = this.optionRepository.GetAll()
                .To<OptionServiceModel>();

            return optionModels;
        }

        public IQueryable<OptionServiceModel> GetAllSorted(
            IQueryable<Option> options,
            IOptionSortStrategy sortStrategy,
            int pageNumber)
        {
            var sortedAndFilteredOptions = sortStrategy.Sort(options);
            var currentPageOptionModels = sortedAndFilteredOptions
                .To<OptionServiceModel>()
                .GetFromPage(pageNumber);

            return currentPageOptionModels;
        }

        public async Task<OptionServiceModel> GetByIdAsync(string id)
        {
            var model = await this.optionRepository
                .FindAll(id)
                .To<OptionServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(OptionServiceModel model)
        {
            await this.adminEditService.EditAsync<Option, OptionServiceModel>(model, model.Id);
        }
    }
}
