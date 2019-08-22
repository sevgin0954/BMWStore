using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Helpers;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.CachedServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInventoryController : Controller
    {
        private readonly ICookiesService cookiesService;
        private readonly INewCarRepository newCarRepository;
        private readonly ICarsService carsService;
        private readonly ICarsFilterTypesService carsFilterTypesService;
        private readonly ICachedCarsFilterTypesService cachedCarsFilterTypesService;

        public NewInventoryController(
            ICookiesService cookiesService,
            INewCarRepository newCarRepository,
            ICarsService carsService,
            ICarsFilterTypesService carsFilterTypesService,
            ICachedCarsFilterTypesService cachedCarsFilterTypesService)
        {
            this.cookiesService = cookiesService;
            this.newCarRepository = newCarRepository;
            this.carsService = carsService;
            this.carsFilterTypesService = carsFilterTypesService;
            this.cachedCarsFilterTypesService = cachedCarsFilterTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarsInventoryBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserNewCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserNewCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<BaseCarSortStrategyType>(cookie, sortTypeKey);

            var key = KeyGenerator.Generate(
                WebConstants.CacheNewInventoryPrepend,
                model,
                model.ModelTypes,
                model.PageNumber.ToString(),
                model.PriceRange,
                model.Series,
                model.Year);

            var sortStrategy = NewCarSortStrategyFactory.GetStrategy<NewCar>(sortType, sortDirection);

            var priceRanges = ParameterParser.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series);

            var filteredCars = this.newCarRepository
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var filterMultipleStrategy = CarMultipleFilterStrategyFactory.GetStrategy(model.ModelTypes);
            var filteredByMultipleCars = filterMultipleStrategy.Filter(sortedAndFilteredCars);

            var currentPageCarModels = await this.carsService
                .GetCarScheduleViewModelAsync<CarInventoryConciseViewModel>(filteredByMultipleCars, this.User, model.PageNumber);
            var filterModel = await this.cachedCarsFilterTypesService
                .GetCachedCarFilterModelAsync(key, sortedAndFilteredCars, filteredByMultipleCars);
            var viewModel = new CarsInventoryViewModel()
            {
                SortStrategyType = sortType,
                SortStrategyDirection = sortDirection,
                Cars = currentPageCarModels,
                CurrentPage = model.PageNumber,
                FilterModel = filterModel,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredByMultipleCars),
                TotalCarsCount = await filteredByMultipleCars.CountAsync()
            };

            this.carsFilterTypesService
                .SelectModelFilterItems(filterModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangeSortType(BaseCarSortStrategyType sortStrategyType, string returnUrl)
        {
            var sortTypeKey = WebConstants.CookieUserNewCarsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection, string returnUrl)
        {
            var sortDirectionKey = WebConstants.CookieUserNewCarsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return Redirect(returnUrl);
        }
    }
}