using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using System.Collections.Generic;

namespace BMWStore.Services.Interfaces
{
    public interface IOptionTypeService
    {
        IEnumerable<OptionTypeViewModel> GetViewModels(IEnumerable<OptionConciseViewModel> optionModels);
    }
}