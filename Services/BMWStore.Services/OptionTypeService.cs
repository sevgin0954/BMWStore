using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Services
{
    public class OptionTypeService : IOptionTypeService
    {
        public IEnumerable<OptionTypeViewModel> GetViewModels(IEnumerable<OptionConciseViewModel> optionModels)
        {
            var models = optionModels.GroupBy(o => o.OptionTypeName).Select(group => new OptionTypeViewModel()
            {
                Name = group.Key,
                OptionNames = group.Select(o => o.Name).ToList()
            });

            return models;
        }
    }
}