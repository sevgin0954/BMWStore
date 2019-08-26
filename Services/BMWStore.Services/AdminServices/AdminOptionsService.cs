using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionsService : IAdminOptionsService
    {
        private readonly IOptionRepository optionRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminOptionsService(
            IOptionRepository optionRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.optionRepository = optionRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, Option>(id);

            return model;
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

        public IQueryable<OptionServiceModel> GetAll()
        {
            var optionModels = this.optionRepository.GetAll()
                .To<OptionServiceModel>();

            return optionModels;
        }

        public async Task CreateNewAsync(OptionServiceModel model)
        {
            var dbOption = Mapper.Map<Option>(model);
            this.optionRepository.Add(dbOption);

            var rowsAffected = await this.optionRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task EditAsync(OptionServiceModel model)
        {
            await this.adminEditService.EditAsync<Option, OptionServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string optionId)
        {
            await this.adminDeleteService.DeleteAsync<Option>(optionId);
        }
    }
}
