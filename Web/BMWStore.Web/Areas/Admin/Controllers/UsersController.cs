using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.UserModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;
        private readonly ICookiesService cookiesService;
        private readonly IUserRepository userRepository;

        public UsersController(
            IAdminUsersService usersService, 
            ICookiesService cookiesService,
            IUserRepository userRepository)
        {
            this.adminUsersService = usersService;
            this.cookiesService = cookiesService;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;
            var sortStrategyType = this.cookiesService
                .GetValueOrDefault<UserSortStrategyType>(cookies, WebConstants.CookieAdminUsersSortTypeKey);
            var sortDirection = this.cookiesService
                .GetValueOrDefault<SortStrategyDirection>(cookies, WebConstants.CookieAdminUsersSortDirectionKey);
            var sortStrategy = UserSortStrategyFactory.GetStrategy(sortStrategyType, sortDirection);

            var allUsers = this.userRepository.GetAll();
            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(allUsers);
            var userServiceModels = await this.adminUsersService.GetSortedUsersAsync(sortStrategy, pageNumber);
            var userViewModels = await userServiceModels.To<UserAdminViewModel>().ToArrayAsync();
            var model = new AdminUsersViewModel()
            {
                CurrentPage = pageNumber,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortStrategyType,
                TotalPagesCount = totalPagesCount,
                Users = userViewModels
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Account(string id)
        {
            var model = await this.adminUsersService.GetUserByIdAsync<UserAdminViewModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminUsersService.DeleteAsync(id);

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