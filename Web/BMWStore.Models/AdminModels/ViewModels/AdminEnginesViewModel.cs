using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Models.EngineModels.ViewModels;
using BMWStore.Models.PaginationModels;
using System.Collections.Generic;

namespace BMWStore.Models.AdminModels.ViewModels
{
    public class AdminEnginesViewModel : BasePaginationModel
    {
        public SortStrategyDirection SortStrategyDirection { get; set; }

        public EngineSortStrategy SortStrategyType { get; set; }

        public IEnumerable<EngineViewModel> Engines { get; set; } = new List<EngineViewModel>();
    }
}
