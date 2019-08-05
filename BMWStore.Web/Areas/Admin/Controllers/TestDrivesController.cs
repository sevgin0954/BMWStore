using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TestDrivesController : BaseAdminController
    {
        private readonly IAdminTestDrivesService adminTestDrivesService;
        private readonly ISortCookieService sortCookieService;

        public TestDrivesController(IAdminTestDrivesService adminTestDrivesService, ISortCookieService sortCookieService)
        {
            this.adminTestDrivesService = adminTestDrivesService;
            this.sortCookieService = sortCookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            var sortType = this.sortCookieService
                .GetSortStrategyTypeOrDefault<AdminTestDrivesSortStrategyType>(cookies, sortTypeKey);

            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            var sortDirection = this.sortCookieService
                .GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey);

            var model = await this.adminTestDrivesService.GetTestDriveViewModelAsync(sortType, sortDirection, pageNumber);

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
            this.sortCookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType, sortTypeKey);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            this.sortCookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection, sortDirectionKey);

            return RedirectToAction("Index");
        }
    }
}