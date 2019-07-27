using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    [Authorize]
    public class TestDrivesController : Controller
    {
        private readonly ITestDriveService testDriveService;

        public TestDrivesController(ITestDriveService testDriveService)
        {
            this.testDriveService = testDriveService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var model = await this.testDriveService.GetTestDriveViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleTestDriveBindingModel model)
        {
            await this.testDriveService.ScheduleTestDriveAsync(model, this.User);

            return RedirectToAction("Index", new { id = model.CarId });
        }
    }
}