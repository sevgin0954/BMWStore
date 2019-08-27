using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.UserModels.ViewModels;
using MappingRegistrar.Interfaces;
using System;
using BMWStore.Models.TestDriveStatusModels.ViewModels;
using BMWStore.Services.Models;

namespace BMWStore.Models.TestDriveModels.ViewModels
{
    public class TestDriveViewModel : IMapFrom<TestDriveServiceModel>
    {
        public string Id { get; set; }

        public TestDriveUserViewModel User { get; set; }

        public CarTestDriveViewModel Car { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Comment { get; set; }

        public StatusViewModel Status { get; set; }
    }
}
