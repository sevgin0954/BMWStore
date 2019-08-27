using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminTransmissionsService : IAdminTransmissionsService
    {
        private readonly ITransmissionRepository transmissionRepository;
        private readonly IAdminEditService adminEditService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminTransmissionsService(
            ITransmissionRepository transmissionRepository,
            IAdminEditService adminEditService,
            IAdminDeleteService adminDeleteService)
        {
            this.transmissionRepository = transmissionRepository;
            this.adminEditService = adminEditService;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task CreateNewAsync(TransmissionServiceModel model)
        {
            var dbTransmission = Mapper.Map<Transmission>(model);

            this.transmissionRepository.Add(dbTransmission);
            var rowsAffected = await this.transmissionRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string transmissionId)
        {
            await this.adminDeleteService.DeleteAsync<Transmission>(transmissionId);
        }

        public IQueryable<TransmissionServiceModel> GetAll()
        {
            var models = this.transmissionRepository
                .GetAll()
                .To<TransmissionServiceModel>();

            return models;
        }

        public async Task<TransmissionServiceModel> GetByIdAsync(string id)
        {
            var model = await this.transmissionRepository
                .FindAll(id)
                .To<TransmissionServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(TransmissionServiceModel model)
        {
            await this.adminEditService.EditAsync<Transmission, TransmissionServiceModel>(model, model.Id);
        }
    }
}