using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminTransmissionsService
    {
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;
        IQueryable<TransmissionServiceModel> GetAll();
        Task CreateNewAsync(TransmissionServiceModel model);
        Task EditAsync(TransmissionServiceModel model);
        Task DeleteAsync(string transmissionId);
    }
}
