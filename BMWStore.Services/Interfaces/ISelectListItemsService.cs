using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ISelectListItemsService
    {
        void SelectItemsWithValues(IEnumerable<SelectListItem> selectListItems, params string[] values);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync<TEntity>() where TEntity : class;
    }
}
