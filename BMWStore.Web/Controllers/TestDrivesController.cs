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

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var models = await this.testDriveService.GetAllTestDrivesAsync(this.User);
            this.ViewData["returnAction"] = "Index";

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Drive(string testDriveId)
        {
            var model = await this.testDriveService.GetTestDriveAsync(testDriveId, this.User);
            this.ViewData["returnAction"] = "Drive";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleTestDriveBindingModel model)
        {
            var testDriveId = await this.testDriveService.ScheduleTestDriveAsync(model, this.User);

            return RedirectToAction("Drive", new { testDriveId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string testDriveId, string returnAction = "Drive", string returnUrl = null)
        {
            await this.testDriveService.CancelTestDriveAsync(testDriveId, this.User);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(returnAction, new { testDriveId });
        }
    }
}