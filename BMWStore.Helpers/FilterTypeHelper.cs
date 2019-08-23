using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Models.FilterModels.BindingModels;
using System;
using System.Collections.Generic;

namespace BMWStore.Helpers
{
    public static class FilterTypeHelper
    {
        public static void SelectFilterTypes(IEnumerable<FilterTypeBindingModel> filterTypes, string value)
        {
            DataValidator.ValidateNotNull(filterTypes, new ArgumentException(ErrorConstants.CantBeNullParameter));
            DataValidator.ValidateNotNullOrEmpty(value, new ArgumentException(ErrorConstants.CantBeNullOrEmpty));

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
