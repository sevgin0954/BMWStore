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
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;
        private readonly ISortCookieService cookieService;

        public UsersController(IAdminUsersService usersService, ISortCookieService cookieService)
        {
            this.adminUsersService = usersService;
            this.cookieService = cookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;
            var model = await this.adminUsersService.GetSortedUsersAsync(cookies, pageNumber);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminUsersService.DeleteUserAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Ban(string id)
        {
            await this.adminUsersService.BanUserAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unban(string id)
        {
            await this.adminUsersService.UnbanUserAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(UserSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminUsersSortTypeKey;
            this.cookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType, sortTypeKey);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminUsersSortDirectionKey;
            this.cookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection, sortDirectionKey);

            return RedirectToAction("Index");
        }
    }
}