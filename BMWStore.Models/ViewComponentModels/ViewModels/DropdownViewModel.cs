using System.Collections.Generic;

namespace BMWStore.Models.ViewComponentModels.ViewModels
{
    public class DropdownViewModel
    {
        public IEnumerable<string> SortNames { get; set; }

        public string SelectedSortName { get; set; }

        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string PrependText { get; set; }

        public string ReturnUrl { get; set; }

        public string ParameterName { get; set; }
    }
}