using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class FuelTypesService : IFuelTypesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public FuelTypesService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewFuelTypeAsync(AdminFuelTypeCreateBindingModel model)
        {
            var dbFuelType = Mapper.Map<FuelType>(model);
            this.unitOfWork.FuelTypes.Add(dbFuelType);
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var dbFuelTypes = await this.unitOfWork.FuelTypes.GetAllAsync();
            var selectListItems = Mapper.Map<IEnumerable<SelectListItem>>(dbFuelTypes);

            return selectListItems;
        }
    }
}
