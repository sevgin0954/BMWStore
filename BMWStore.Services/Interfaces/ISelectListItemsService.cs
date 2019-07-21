using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMWStore.Services.Interfaces
{
    public interface ISelectListItemsService
    {
        void SelectItemsWithValues(IEnumerable<SelectListItem> selectListItems, params string[] values);
    }
}
