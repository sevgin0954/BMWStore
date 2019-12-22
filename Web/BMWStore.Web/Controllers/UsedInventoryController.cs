using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Web.Factories.FilterStrategyFactory;
using BMWStore.Web.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class UsedInventoryController : Controller
    {
        private readonly ICookiesService cookiesService;
        private readonly IUsedCarRepository usedCarRepository;
        private readonly ICarsService carsService;
        private readonly ICarsFilterTypesService carsFilterTypesService;
        private readonly ICacheService cacheService;
		private readonly ICarTestDriveService carTestDriveService;

		public UsedInventoryController(
            ICookiesService cookiesService,
            IUsedCarRepository usedCarRepository,
            ICarsService carsService,
            ICarsFilterTypesService carsFilterTypesService,
            ICacheService cacheService,
			ICarTestDriveService carTestDriveService)
        {
            this.cookiesService = cookiesService;
            this.usedCarRepository = usedCarRepository;
            this.carsService = carsService;
            this.carsFilterTypesService = carsFilterTypesService;
            this.cacheService = cacheService;
			this.carTestDriveService = carTestDriveService;
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

            var filteredCars = this.carsService
                .GetFiltered<UsedCar>(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var filterMultipleStrategy = CarMultipleFilterStrategyFactory.GetStrategy(model.ModelTypes);
            var filteredByMultipleCars = filterMultipleStrategy.Filter(sortedAndFilteredCars);

            var currentPageViewModels = await (await this.carTestDriveService
				.GetCarTestDriveModelAsync<CarConciseTestDriveServiceModel>(filteredByMultipleCars, this.User, model.PageNumber))
                .To<CarInventoryConciseViewModel>()
                .ToArrayAsync();

            var key = KeyGenerator.Generate(
                WebConstants.CacheUsedInventoryPrepend,
                model.ModelTypes,
                model.PageNumber.ToString(),
                model.PriceRange,
                model.Series,
                model.Year);
            CarsFilterViewModel filterViewModel;
            var cachedModel = await this.cacheService.GetOrDefaultAsync<CarsFilterViewModel>(key);
            if (cachedModel != null)
            {
                filterViewModel = cachedModel;
            }
            else
            {
                var filterServiceModel = await this.carsFilterTypesService
                    .GetCarFilterModelAsync(sortedAndFilteredCars, filteredByMultipleCars);
                filterViewModel = Mapper.Map<CarsFilterViewModel>(filterServiceModel);
            }

            var viewModel = new CarsInventoryViewModel()
            {
                SortStrategyType = sortType,
                SortStrategyDirection = sortDirection,
                Cars = currentPageViewModels,
                CurrentPage = model.PageNumber,
                FilterModel = filterViewModel,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredByMultipleCars),
                TotalCarsCount = await filteredByMultipleCars.CountAsync()
            };

            FilterTypeHelper.SelectFilterTypes(filterViewModel.Years, model.Year);
            FilterTypeHelper.SelectFilterTypes(filterViewModel.Series, model.Series);
            FilterTypeHelper.SelectFilterTypes(filterViewModel.ModelTypes, model.ModelTypes.ToArray());
            FilterTypeHelper.SelectFilterTypes(filterViewModel.Prices, model.PriceRange);

            _ = this.cacheService.AddInfinityCacheAsync(filterViewModel, key, WebConstants.CacheCarsType);

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