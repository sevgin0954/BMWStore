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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(TestDriveStatus status, string id)
        {
            await this.adminTestDrivesService.ChangeTestDriveStatusAsync(status, id, this.User);

            return RedirectToAction("Index");
        }
    }
}