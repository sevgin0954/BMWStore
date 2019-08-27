using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTransmissionsService
    {
        Task CreateNewAsync(TransmissionServiceModel model);
        Task DeleteAsync(string transmissionId);
        IQueryable<TransmissionServiceModel> GetAll();
        Task<TransmissionServiceModel> GetByIdAsync(string id);
        Task EditAsync(TransmissionServiceModel model);
    }
}
