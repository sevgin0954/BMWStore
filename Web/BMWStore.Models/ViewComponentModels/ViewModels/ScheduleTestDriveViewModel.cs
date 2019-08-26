using BMWStore.Models.TestDriveModels.BindingModels;

namespace BMWStore.Models.ViewComponentModels.ViewModels
{
    public class ScheduleTestDriveViewModel
    {
        public bool IsTestDriveScheduled { get; set; }

        public string TestDriveId { get; set; }

        public string CarId { get; set; }

        public ScheduleTestDriveBindingModel TestDriveModel { get; set; }
    }
}