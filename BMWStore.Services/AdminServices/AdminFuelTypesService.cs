using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminFuelTypesService : IAdminFuelTypesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminFuelTypesService(IBMWStoreUnitOfWork unitOfWork)
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
            var selectListItems = await this.unitOfWork.FuelTypes
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }
    }
}
