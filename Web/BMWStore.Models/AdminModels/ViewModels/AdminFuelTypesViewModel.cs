using BMWStore.Models.FuelTypeModels.ViewModels;
using BMWStore.Models.PaginationModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminFuelTypesViewModel : BasePaginationModel
    {
        public IEnumerable<FuelTypeViewModel> FuelTypes { get; set; } = new List<FuelTypeViewModel>();
    }
}
