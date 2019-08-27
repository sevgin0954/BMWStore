using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Services.Models
{
    public class TestDriveServiceModel : IMapFrom<TestDrive>
    {
        public string Id { get; set; }

        public string CarId { get; set; }
        public CarConciseServiceModel Car { get; set; }

        public string UserId { get; set; }
        public UserServiceModel User { get; set; }

        public string StatusId { get; set; }
        public StatusServiceModel Status { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Comment { get; set; }
    }
}
