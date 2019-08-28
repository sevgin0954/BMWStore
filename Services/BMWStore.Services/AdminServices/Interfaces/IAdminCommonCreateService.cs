using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminCommonCreateService
    {
        Task CreateAsync<TEntity, TModel>(TModel creatingModel)
            where TEntity : class
            where TModel : class;
    }
}
