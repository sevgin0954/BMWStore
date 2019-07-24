using System.ComponentModel.DataAnnotations.Schema;

namespace BMWStore.Models.FilterModels.BindingModels
{
    public class FilterTypeBindingModel
    {
        [NotMapped]
        public bool IsSelected { get; set; }

        public string Value { get; set; }

        public int CarsCount { get; set; }

        public string Text { get; set; }
    }
}
