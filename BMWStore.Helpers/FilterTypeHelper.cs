using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Models.FilterModels.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Helpers
{
    public static class FilterTypeHelper
    {
        public static void SelectFilterTypes(IEnumerable<FilterTypeBindingModel> filterTypes, params string[] values)
        {
            DataValidator.ValidateNotNull(filterTypes, new ArgumentException(ErrorConstants.CantBeNullParameter));
            DataValidator.ValidateNotNull(filterTypes, new ArgumentException(ErrorConstants.CantBeNullParameter));

            foreach (var filterType in filterTypes)
            {
                if (values.Contains(filterType.Value))
                {
                    filterType.IsSelected = true;
                }
            }
        }
    }
}
