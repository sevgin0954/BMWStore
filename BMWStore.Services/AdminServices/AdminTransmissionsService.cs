using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminTransmissionsService : IAdminTransmissionsService
    {
        private readonly ITransmissionRepository transmissionRepository;

        public AdminTransmissionsService(ITransmissionRepository transmissionRepository)
        {
            this.transmissionRepository = transmissionRepository;
        }

        public async Task CreateNewTransmissionAsync(AdminTransmissionsCreateBindingModel model)
        {
            var dbTransmission = Mapper.Map<Transmission>(model);

            this.transmissionRepository.Add(dbTransmission);
            var rowsAffected = await this.transmissionRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
