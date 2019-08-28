using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Services.Models
{
    public class ScheduleTestDriveServiceModel : IMapTo<TestDrive>
    {
        public string CarId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Comment { get; set; }
    }
}
