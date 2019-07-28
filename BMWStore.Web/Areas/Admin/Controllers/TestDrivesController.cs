using BMWStore.Common.Enums;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TestDrivesController : BaseAdminController
    {
        private readonly IAdminTestDrivesService adminTestDrivesService;

        public TestDrivesController(IAdminTestDrivesService adminTestDrivesService)
        {
            this.adminTestDrivesService = adminTestDrivesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.adminTestDrivesService.GetAllTestDrivesAsync();
            this.ViewData["returnUrl"] = "/Admin/TestDrives";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Check(string testDriveId)
        {
            await this.adminTestDrivesService.CheckTestDriveStatusAsync(testDriveId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string testDriveId)
        {
            await this.adminTestDrivesService.DeleteAsync(testDriveId);

            return RedirectToAction("Index");
        }
    }
}