using AutoMapper;
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
        private readonly IAdminEditService adminEditService;

        public AdminOptionTypesService(
            IOptionTypeRepository optionTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.optionTypeRepository = optionTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task<IEnumerable<OptionTypeConciseViewModel>> GetAllAsync()
        {
            var models = await this.readService.GetAllAsync<OptionTypeConciseViewModel, OptionType>();

            return models;
        }

        public async Task CreateOptionTypeAsync(OptionTypeBindingModel model)
        {
            var dbOptionType = Mapper.Map<OptionType>(model);
            this.optionTypeRepository.Add(dbOptionType);

            var rowsAffected = await this.optionTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<OptionTypeBindingModel> GetEditingModelAsync(string optionTypeId)
        {
            var model = await this.readService.GetModelByIdAsync<OptionTypeBindingModel, OptionType>(optionTypeId);

            return model;
        }

        public async Task EditAsync(OptionTypeBindingModel model)
        {
            await this.adminEditService.EditAsync<OptionType, OptionTypeBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string optionTypeId)
        {
            await this.adminDeleteService.DeleteAsync<OptionType>(optionTypeId);
        }
    }
}