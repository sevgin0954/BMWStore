using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInvertoryController : Controller
    {
        private readonly INewCarsInvertoryService newCarsService;
        private readonly ISortCookieService sortCookieService;

        public NewInvertoryController(INewCarsInvertoryService newCarsService, ISortCookieService sortCookieService)
        {
            this.newCarsService = newCarsService;
            this.sortCookieService = sortCookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<NewBaseCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = NewCarSortStrategyFactory.GetStrategy(sortType, sortDirection);
            var cars = await this.newCarsService.GetAllAsync(sortStrategy);
            var model = new NewCarsInvertoryViewModel()
            {
                Cars = cars,
                SortStrategyType = sortType,
                SortStrategyDirection = sortDirection
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeSortType(NewBaseCarSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            this.sortCookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType, sortTypeKey);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            this.sortCookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection, sortDirectionKey);

            return RedirectToAction("Index");
        }
    }
}