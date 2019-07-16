using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class ModelTypesService : IModelTypesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public ModelTypesService(IBMWStoreUnitOfWork unitOfWork)
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
            var dbModelTypes = await this.unitOfWork.ModelTypes.GetAllAsync();
            var selectListItems = Mapper.Map<IEnumerable<SelectListItem>>(dbModelTypes);

            return selectListItems;
        }
    }
}
