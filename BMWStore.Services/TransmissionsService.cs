using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class TransmissionsService : ITransmissionsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public TransmissionsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var dbTransmissions = await this.unitOfWork.Transmissions.GetAllAsync();

            var selectListItems = Mapper.Map<IEnumerable<SelectListItem>>(dbTransmissions);

            return selectListItems;
        }

        public async Task CreateNewTransmissionAsync(AdminTransmissionsCreateBindingModel model)
        {
            var dbTransmission = Mapper.Map<Transmission>(model);

            this.unitOfWork.Transmissions.Add(dbTransmission);
            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
