using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
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
            IEnumerable<string> modelTypes,
            string year = WebConstants.AllFilterTypeModelValue,
            string priceRange = WebConstants.AllFilterTypeModelValue,
            string series = WebConstants.AllFilterTypeModelValue)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<NewBaseCarSortStrategyType>(cookie, sortTypeKey);
            var sortStrategy = NewCarSortStrategyFactory.GetStrategy(sortType, sortDirection);

            var priceRanges = this.ParsePriceRange(priceRange);
            var parsedYear = this.ParseParameter(year);
            var parsedSeries = this.ParseParameter(series);
            var filterStrategy = CarFilterStrategyFactory
                .GetStrategies(parsedYear, priceRanges[0], priceRanges[1], parsedSeries, modelTypes);

            var model = await this.newCarsInvertoryService.GetInvertoryBindingModel(sortStrategy, filterStrategy.ToArray());
            this.SelectModelFilterItems(model, year?.ToString(), priceRange, series, modelTypes);

            return View(model);
        }

        private void SelectModelFilterItems(NewCarsInvertoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes)
        {
            this.SelectFilterTypeModel(model.Years, year);
            this.SelectFilterTypeModel(model.Series, series);
            this.SelectFilterTypeModel(model.ModelTypes, modelTypes.ToArray());
            this.SelectFilterTypeModel(model.Prices, priceRange);
        }

        private void SelectFilterTypeModel(IEnumerable<FilterTypeBindingModel> filterTypeBindingModels, params string[] values)
        {
            if (values != null && values.Length > 0)
            {
                foreach (var model in filterTypeBindingModels)
                {
                    if (values.Contains(model.Value))
                    {
                        model.IsSelected = true;
                    }
                }
            }
        }

        private decimal?[] ParsePriceRange(string priceRange)
        {
            var priceRanges = new decimal?[] { null, null };

            if (priceRange != null && priceRange != WebConstants.AllFilterTypeModelValue)
            {
                var priceParts = priceRange
                    .Split(" -}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (priceParts.Length == 2)
                {
                    priceRanges[0] = decimal.Parse(priceParts[0]);
                    priceRanges[1] = decimal.Parse(priceParts[1]);
                }
            }

            return priceRanges;
        }

        private string ParseParameter(string paramValue)
        {
            if (paramValue == null || paramValue == WebConstants.AllFilterTypeModelValue)
            {
                return null;
            }

            return paramValue;
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