using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using MappingRegistrar.Interfaces;
using System;

namespace BMWStore.Models.TestDriveModels.ViewModels
{
    public class TestDriveViewModel : IMapFrom<TestDrive>
    {
        public TestDriveCarViewModel Car { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Comment { get; set; }
    }
}
