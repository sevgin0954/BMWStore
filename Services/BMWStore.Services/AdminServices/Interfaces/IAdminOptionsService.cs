using BMWStore.Services.SortStrategies.OptionStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionsService
    {
        Task CreateNewAsync(OptionServiceModel model);
        Task DeleteAsync(string optionId);
        IQueryable<OptionServiceModel> GetAll();
        IQueryable<OptionServiceModel> GetAllSorted(
           IQueryable<Option> options,
           IOptionSortStrategy sortStrategy,
           int pageNumber);
        Task<OptionServiceModel> GetByIdAsync(string id);
        Task EditAsync(OptionServiceModel model);
    }
}
