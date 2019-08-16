using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;
        private readonly ICookiesService cookiesService;

        public UsersController(IAdminUsersService usersService, ICookiesService cookiesService)
        {
            this.adminUsersService = usersService;
            this.cookiesService = cookiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;
            var model = await this.adminUsersService.GetSortedUsersAsync(cookies, pageNumber);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Account(string id)
        {
            var model = await this.adminUsersService.GetUserByIdAsync(id);

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
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminUsersSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}