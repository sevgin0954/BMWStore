using BMWStore.Common.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.FilterModels.BindingModels
{
    public class FilterTypeBindingModel : ISelectable, IMapFrom<FilterTypeServiceModel>, IMapTo<FilterTypeBindingModel>
    {
        public int CarsCount { get; set; }

        public bool IsSelected { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}
