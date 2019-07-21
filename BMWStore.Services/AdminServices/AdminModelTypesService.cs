using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminModelTypesService : IAdminModelTypesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminModelTypesService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewModelType(AdminModelTypeCreateBidningModel model)
        {
            var dbModelType = Mapper.Map<ModelType>(model);
            this.unitOfWork.ModelTypes.Add(dbModelType);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.unitOfWork.ModelTypes
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }
    }
}
