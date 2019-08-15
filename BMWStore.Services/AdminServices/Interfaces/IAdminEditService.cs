using BMWStore.Entities;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminEditService
    {
        Task EditAsync<TEntity, TModel>(TModel editingModel, string id)
            where TEntity : BaseEntity
            where TModel : class;
    }
}
