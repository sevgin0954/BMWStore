using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TestDrivesController : BaseAdminController
    {
        private readonly IAdminTestDrivesService adminTestDrivesService;
        private readonly ICookiesService cookiesService;

        public TestDrivesController(
            IAdminTestDrivesService adminTestDrivesService, 
            ICookiesService ookiesService)
        {
            this.adminTestDrivesService = adminTestDrivesService;
            this.cookiesService = ookiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<AdminTestDrivesSortStrategyType>(cookies, sortTypeKey);

            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);

            var model = await this.adminTestDrivesService.GetTestDrivesViewModelAsync(sortType, sortDirection, pageNumber);

            this.ViewData["returnUrl"] = "/Admin/TestDrives";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Check(string testDriveId)
        {
            await this.adminTestDrivesService.ChangeTestDriveStatusToPassedAsync(testDriveId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string testDriveId)
        {
            await this.adminTestDrivesService.DeleteAsync(testDriveId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(AdminTestDrivesSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}