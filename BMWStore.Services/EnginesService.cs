using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class EnginesService : IEnginesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public EnginesService(IBMWStoreUnitOfWork unitOfWork)
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
            // Use automapper with queriable
            var dbEngines = await this.unitOfWork.Engines.GetAllAsync();
            var selectListItems = Mapper.Map<IEnumerable<SelectListItem>>(dbEngines);

            return selectListItems;
        }
    }
}
