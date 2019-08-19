using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Helpers;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class UsedInventoryController : Controller
    {
        private readonly ICarsInventoryService carsInventoryService;
        private readonly ICookiesService cookiesService;
        private readonly IUsedCarRepository usedCarRepository;

        public UsedInventoryController(
            ICarsInventoryService carsInventoryService,
            ICookiesService cookiesService,
            IUsedCarRepository usedCarRepository)
        {
            this.carsInventoryService = carsInventoryService;
            this.cookiesService = cookiesService;
            this.usedCarRepository = usedCarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarsInventoryBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserUsedCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserUsedCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<UsedCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = UsedCarSortStrategyFactory.GetStrategy<UsedCar>(sortType, sortDirection);

            var priceRanges = ParameterParser.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series);

            var filteredCars = this.usedCarRepository
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var filterStrategy = CarMultipleFilterStrategyFactory.GetStrategy(model.ModelTypes);
            var viewModel = await this.carsInventoryService
                .GetInventoryViewModelAsync(filterStrategy, sortedAndFilteredCars, this.User, model.PageNumber);
            this.carsInventoryService.SelectModelFilterItems(viewModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);

            viewModel.SortStrategyDirection = sortDirection;
            viewModel.SortStrategyType = sortType;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangeSortType(UsedCarSortStrategyType sortStrategyType, string returnUrl)
        {
            var sortTypeKey = WebConstants.CookieUserUsedCarsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection, string returnUrl)
        {
            var sortDirectionKey = WebConstants.CookieUserUsedCarsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return Redirect(returnUrl);
        }
    }
}