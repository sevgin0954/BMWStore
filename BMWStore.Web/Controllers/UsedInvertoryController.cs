using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class UsedInvertoryController : Controller
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly ICarsInvertoryService carsInvertoryService;
        private readonly ISortCookieService sortCookieService;

        public UsedInvertoryController(IBMWStoreUnitOfWork unitOfWork,
            ICarsInvertoryService carsInvertoryService,
            ISortCookieService sortCookieService)
        {
            this.unitOfWork = unitOfWork;
            this.carsInvertoryService = carsInvertoryService;
            this.sortCookieService = sortCookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarsInvertoryBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<UsedCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = UsedCarSortStrategyFactory.GetStrategy<UsedCar>(sortType, sortDirection);

            var priceRanges = this.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series, model.ModelTypes);

            var filteredCars = this.unitOfWork.UsedCars
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);
            var viewModel = await this.carsInvertoryService.GetInvertoryBindingModel(sortedAndFilteredCars, sortType, sortDirection);
            this.carsInvertoryService.SelectModelFilterItems(viewModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);

            return View(viewModel);
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

        [HttpPost]
        public IActionResult ChangeSortType(UsedCarSortStrategyType sortStrategyType, string returnUrl)
        {
            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            this.sortCookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType, sortTypeKey);

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection, string returnUrl)
        {
            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            this.sortCookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection, sortDirectionKey);

            return Redirect(returnUrl);
        }
    }
}