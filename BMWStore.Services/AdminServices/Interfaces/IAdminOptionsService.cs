using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.FilterStrategies.OptionStrategies.Interfaces;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.OptionModels.BidningModels;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionsService
    {
        Task<AdminOptionsViewModel> GetOptionsViewModelAsync(
            IOptionFilterStrategy filterStrategy,
            OptionSortStrategyType sortStrategType,
            SortStrategyDirection sortDirection,
            int pageNumber);
        Task CreateNewOptionAsync(AdminOptionCreateBindingModel model);
        Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string carOptionId);
        Task EditOptionAsync(AdminCarOptionEditBindingModel model);
        Task DeleteAsync(string optionId);
    }
}
