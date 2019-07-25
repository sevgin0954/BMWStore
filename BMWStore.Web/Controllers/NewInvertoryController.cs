using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInvertoryController : Controller
    {
        private readonly INewCarsInvertoryService newCarsInvertoryService;
        private readonly ISortCookieService sortCookieService;

        public NewInvertoryController(INewCarsInvertoryService newCarsInvertoryService, ISortCookieService sortCookieService)
        {
            this.newCarsInvertoryService = newCarsInvertoryService;
            this.sortCookieService = sortCookieService;
        }

        // TODO: REFACTOR
        [HttpGet]
        public async Task<IActionResult> Index(
            int? year, 
            string priceRange,
            string series,
            IEnumerable<string> modelTypes)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<NewBaseCarSortStrategyType>(cookie, sortTypeKey);
            var sortStrategy = NewCarSortStrategyFactory.GetStrategy(sortType, sortDirection);

            decimal?[] priceRanges = this.ParsePriceRange(priceRange);
            var filterStrategy = CarFilterStrategyFactory.GetStrategies(year, priceRanges[0], priceRanges[1], series, modelTypes);

            var model = await this.newCarsInvertoryService.GetInvertoryBindingModel(sortStrategy, filterStrategy.ToArray());

            return View(model);
        }

        private decimal?[] ParsePriceRange(string priceRange)
        {
            var priceRanges = new decimal?[] { null, null };

            if (priceRange != null)
            {
                var priceParts = priceRange.Split(" -".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                priceRanges[0] = decimal.Parse(priceParts[0]);
                priceRanges[1] = decimal.Parse(priceParts[1]);
            }

            return priceRanges;
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