using BMWStore.Common.Enums;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;
        private readonly IAdminSortCookieService cookieService;

        public UsersController(IAdminUsersService usersService, IAdminSortCookieService cookieService)
        {
            this.adminUsersService = usersService;
            this.cookieService = cookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sortStrategyName = this.cookieService.GetSortStrategyTypeOrDefault(this.HttpContext.Request.Cookies);
            var sortDirection = this.cookieService.GetSortStrategyDirectionOrDefault(this.HttpContext.Request.Cookies);
            var sortStrategy = UserSortStrategyFactory.GetStrategy(sortStrategyName, sortDirection);
            var model = new AdminUsersViewModel()
            {
                Users = await this.adminUsersService.GetAllUsersAsync(sortStrategy),
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortStrategyName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeSortType(UserSortStrategyType sortStrategyType)
        {
            this.cookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            this.cookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection);

            return RedirectToAction("Index");
        }
    }
}