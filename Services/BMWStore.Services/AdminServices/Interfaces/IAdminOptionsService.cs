using BMWStore.Data.SortStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionsService
    {
        Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class;

        IQueryable<OptionServiceModel> GetAllSorted(
           IQueryable<Option> options,
           IOptionSortStrategy sortStrategy,
           int pageNumber);
        IQueryable<OptionServiceModel> GetAll();
        Task CreateNewAsync(OptionServiceModel model);
        Task EditAsync(OptionServiceModel model);
        Task DeleteAsync(string optionId);
    }
}
