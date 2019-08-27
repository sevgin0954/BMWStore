using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionTypesService : IAdminOptionTypesService
    {
        private readonly IOptionTypeRepository optionTypeRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminOptionTypesService(
            IOptionTypeRepository optionTypeRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.optionTypeRepository = optionTypeRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public IQueryable<OptionTypeServiceModel> GetAll()
        {
            var models = this.optionTypeRepository.GetAll().To<OptionTypeServiceModel>();

            return models;
        }

        public async Task CreateNewAsync(OptionTypeServiceModel model)
        {
            var dbOptionType = Mapper.Map<OptionType>(model);
            this.optionTypeRepository.Add(dbOptionType);

            var rowsAffected = await this.optionTypeRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string optionTypeId)
        {
            await this.adminDeleteService.DeleteAsync<OptionType>(optionTypeId);
        }

        public async Task<OptionTypeServiceModel> GetByIdAsync(string id)
        {
            var model = await this.optionTypeRepository
                .FindAll(id)
                .To<OptionTypeServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(OptionTypeServiceModel model)
        {
            await this.adminEditService.EditAsync<OptionType, OptionTypeServiceModel>(model, model.Id);
        }
    }
}