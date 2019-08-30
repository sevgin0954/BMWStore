using BMWStore.Common.Constants;
using BMWStore.Common.Interfaces;
using BMWStore.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Helpers
{
    public static class FilterTypeHelper
    {
        public static void SelectFilterTypes(IEnumerable<ISelectable> filterTypes, params string[] values)
        {
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
