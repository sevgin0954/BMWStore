using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Models.PaginationModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminOptionsViewModel : BasePaginationModel
    {
        public IEnumerable<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();
    }
}
