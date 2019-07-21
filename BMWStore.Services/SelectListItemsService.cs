using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Services
{
    public class SelectListItemsService : ISelectListItemsService
    {
        public void SelectItemsWithValues(IEnumerable<SelectListItem> selectListItems, params string[] values)
        {
            foreach (var selectListItem in selectListItems)
            {
                if (values.Contains(selectListItem.Value))
                {
                    selectListItem.Selected = true;
                }
            }
        }
    }
}
