using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;

namespace BMWStore.Services.Interfaces
{
    public interface IFilterTypesService
    {
        void SelectFilterTypeModelsWithValues(
            IEnumerable<FilterTypeBindingModel> filterTypeBindingModels,
            params string[] values);
    }
}
