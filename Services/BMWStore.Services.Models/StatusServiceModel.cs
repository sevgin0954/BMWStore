using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class StatusServiceModel : IMapFrom<Status>
    {
        public string Name { get; set; }

        public ICollection<TestDriveServiceModel> TestDrives { get; set; } = new List<TestDriveServiceModel>();
    }
}
