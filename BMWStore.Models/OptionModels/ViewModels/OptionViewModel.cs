using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.OptionModels.ViewModels
{
    public class OptionViewModel : IMapFrom<Option>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
