using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Services
{
    public class FilterTypesService : IFilterTypesService
    {
        public void SelectFilterTypeModelsWithValues(
            IEnumerable<FilterTypeBindingModel> filterTypeBindingModels, 
            params string[] values)
        {
            if (values != null && values.Length > 0)
            {
                foreach (var model in filterTypeBindingModels)
                {
                    if (values.Contains(model.Value))
                    {
                        model.IsSelected = true;
                    }
                }
            }
        }
    }
}
