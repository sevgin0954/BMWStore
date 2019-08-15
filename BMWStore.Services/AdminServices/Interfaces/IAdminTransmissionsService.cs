using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Models.TransmissionsModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTransmissionsService
    {
        Task<IEnumerable<TransmissionViewModel>> GetAllAsync();
        Task CreateNewTransmissionAsync(TransmissionCreateBindingModel model);
        Task<TransmissionEditBindingModel> GetEditingModelAsync(string transmissionId);
        Task EditAsync(TransmissionEditBindingModel model);
        Task DeleteService(string transmissionId);
    }
}
