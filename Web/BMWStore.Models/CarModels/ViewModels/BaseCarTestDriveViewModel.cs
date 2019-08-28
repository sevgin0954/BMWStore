using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public abstract class BaseCarTestDriveViewModel : BaseCarViewModel, IMapFrom<BaseCarTestDriveServiceModel>
    {
        public bool IsTestDriveScheduled { get; set; }

        public string TestDriveId { get; set; }
    }
}