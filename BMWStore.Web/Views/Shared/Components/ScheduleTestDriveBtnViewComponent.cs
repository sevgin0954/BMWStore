using BMWStore.Models.TestDriveModels.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace BMWStore.Web.Views.Shared.Components
{
    public class ScheduleTestDriveBtnViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string carId)
        {
            var model = new ScheduleTestDriveBindingModel()
            {
                CarId = carId
            };

            return View(model);
        }
    }
}