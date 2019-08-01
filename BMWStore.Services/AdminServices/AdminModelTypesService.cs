using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminModelTypesService : IAdminModelTypesService
    {
        private readonly IModelTypeRepository modelTypeRepository;

        public AdminModelTypesService(IModelTypeRepository modelTypeRepository)
        {
            this.modelTypeRepository = modelTypeRepository;
        }

        public async Task CreateNewModelType(AdminModelTypeCreateBidningModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.modelTypeRepository.Add(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<ModelTypeViewModel>> GetAllAsync()
        {
            var models = await this.modelTypeRepository
                .GetAll()
                .To<ModelTypeViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.modelTypeRepository
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }

        public async Task DeleteAsync(string modelTypeId)
        {
            var dbModelType = await this.modelTypeRepository.GetByIdAsync(modelTypeId);
            DataValidator.ValidateNotNull(dbModelType, new ArgumentException(ErrorConstants.IncorrectId));

            this.modelTypeRepository.Remove(dbModelType);

            var rowsAffected = await this.modelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
