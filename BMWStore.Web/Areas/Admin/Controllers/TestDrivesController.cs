using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
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
        public async Task<IActionResult> Index(
            AdminTestDriveFilterStrategy filter = AdminTestDriveFilterStrategy.All,
            int pageNumber = 1,
            string name = "")
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<AdminTestDrivesSortStrategyType>(cookies, sortTypeKey);

            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);

            var filterStrategy = AdminTestDrivesFilterStrategyFactory.GetStrategy(filter, name);
            var model = await this.adminTestDrivesService.GetTestDrivesViewModelAsync(
                filterStrategy, 
                sortType,
                sortDirection, 
                pageNumber);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Check(string testDriveId)
        {
            await this.adminTestDrivesService.ChangeTestDriveStatusToPassedAsync(testDriveId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminTestDrivesService.DeleteAsync(id);

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