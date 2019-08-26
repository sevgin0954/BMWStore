using System.Collections.Generic;

namespace BMWStore.Models.OptionTypeModels.ViewModels
{
    public class OptionTypeViewModel
    {
        public string Name { get; set; }

        public IEnumerable<string> OptionNames { get; set; } = new List<string>();
    }
}
