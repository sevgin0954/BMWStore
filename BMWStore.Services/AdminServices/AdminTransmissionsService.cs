using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Models.TransmissionsModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminTransmissionsService : IAdminTransmissionsService
    {
        private readonly ITransmissionRepository transmissionRepository;
        private readonly IReadService readService;
        private readonly IAdminEditService adminEditService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminTransmissionsService(
            ITransmissionRepository transmissionRepository,
            IReadService readService,
            IAdminEditService adminEditService,
            IAdminDeleteService adminDeleteService)
        {
            this.transmissionRepository = transmissionRepository;
            this.readService = readService;
            this.adminEditService = adminEditService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task<IEnumerable<TransmissionViewModel>> GetAllAsync()
        {
            var models = await this.readService.GetAllAsync<TransmissionViewModel, Transmission>();

            return models;
        }

        public async Task CreateNewTransmissionAsync(TransmissionCreateBindingModel model)
        {
            var dbTransmission = Mapper.Map<Transmission>(model);

            this.transmissionRepository.Add(dbTransmission);
            var rowsAffected = await this.transmissionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<TransmissionEditBindingModel> GetEditingModelAsync(string transmissionId)
        {
            var model = await this.readService.GetModelByIdAsync<TransmissionEditBindingModel, Transmission>(transmissionId);

            return model;
        }

        public async Task EditAsync(TransmissionEditBindingModel model)
        {
            await this.adminEditService.EditAsync<Transmission, TransmissionEditBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string transmissionId)
        {
            await this.adminDeleteService.DeleteAsync<Transmission>(transmissionId);
        }
    }
}
