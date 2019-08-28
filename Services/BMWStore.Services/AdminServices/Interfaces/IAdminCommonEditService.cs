using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCommonEditService
    {
        Task EditAsync<TEntity, TModel>(TModel editingModel, string id)
            where TEntity : class
            where TModel : class;
    }
}
