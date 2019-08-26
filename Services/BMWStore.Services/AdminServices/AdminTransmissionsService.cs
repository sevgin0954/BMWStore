using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminTransmissionsService : IAdminTransmissionsService
    {
        private readonly ITransmissionRepository transmissionRepository;
        private readonly IAdminEditService adminEditService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IReadService readService;

        public AdminTransmissionsService(
            ITransmissionRepository transmissionRepository,
            IAdminEditService adminEditService,
            IAdminDeleteService adminDeleteService,
            IReadService readService)
        {
            this.transmissionRepository = transmissionRepository;
            this.adminEditService = adminEditService;
            this.adminDeleteService = adminDeleteService;
            this.readService = readService;
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, Transmission>(id);

            return model;
        }

        public IQueryable<TransmissionServiceModel> GetAll()
        {
            var models = this.transmissionRepository
                .GetAll()
                .To<TransmissionServiceModel>();

            return models;
        }

        public async Task CreateNewAsync(TransmissionServiceModel model)
        {
            var dbTransmission = Mapper.Map<Transmission>(model);

            this.transmissionRepository.Add(dbTransmission);
            var rowsAffected = await this.transmissionRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task EditAsync(TransmissionServiceModel model)
        {
            await this.adminEditService.EditAsync<Transmission, TransmissionServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string transmissionId)
        {
            await this.adminDeleteService.DeleteAsync<Transmission>(transmissionId);
        }
    }
}