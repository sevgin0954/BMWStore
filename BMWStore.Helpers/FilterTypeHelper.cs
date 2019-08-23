using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Helpers
{
    public static class FilterTypeHelper
    {
        public static void SelectFilterTypes(IEnumerable<FilterTypeBindingModel> filterTypes, string value)
        {
            foreach (var filterType in filterTypes)
            {
                if (filterType.Value == value)
                {
                    filterType.IsSelected = true;
                }
            }
        }
    }
}
