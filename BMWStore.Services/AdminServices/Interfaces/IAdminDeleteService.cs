using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminDeleteService
    {
        Task DeleteAsync<TEntity>(string id) where TEntity : class;
    }
}
