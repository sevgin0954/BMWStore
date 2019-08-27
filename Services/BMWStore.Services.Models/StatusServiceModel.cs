using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Services.Models
{
    public class StatusServiceModel : IMapFrom<Status>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
