using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminTransmissionsService : IAdminTransmissionsService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminTransmissionsService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.unitOfWork.Transmissions
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

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
