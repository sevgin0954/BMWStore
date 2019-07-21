using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminEnginesService : IAdminEnginesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminEnginesService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewEngineAsync(AdminEngineCreateBindingModel model)
        {
            var dbEngine = Mapper.Map<Engine>(model);
            this.unitOfWork.Engines.Add(dbEngine);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.unitOfWork.Engines
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }
    }
}
