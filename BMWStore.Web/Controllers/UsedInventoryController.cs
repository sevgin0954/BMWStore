using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Helpers;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class UsedInventoryController : Controller
    {
        private readonly IDistributedCache cache;
        private readonly ICookiesService cookiesService;
        private readonly IUsedCarRepository usedCarRepository;
        private readonly ICarsService carsService;
        private readonly ICarsFilterTypesService carsFilterTypesService;

        public UsedInventoryController(
            IDistributedCache cache,
            ICookiesService cookiesService,
            IUsedCarRepository usedCarRepository,
            ICarsService carsService,
            ICarsFilterTypesService carsFilterTypesService)
        {
            this.cache = cache;
            this.cookiesService = cookiesService;
            this.usedCarRepository = usedCarRepository;
            this.carsService = carsService;
            this.carsFilterTypesService = carsFilterTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarsInventoryBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserUsedCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserUsedCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<UsedCarSortStrategyType>(cookie, sortTypeKey);

            var key = KeyGenerator.Generate(
                WebConstants.CacheUsedInventoryPrepend,
                model,
                model.ModelTypes,
                model.PageNumber.ToString(),
                model.PriceRange,
                model.Series,
                model.Year,
                sortDirection.ToString(),
                sortType.ToString());

            var cachedModelAsBytes = await this.cache.GetAsync(key);
            if (cachedModelAsBytes != null)
            {
                var modelAsUsedViewModel = JSonHelper.Desirialize<NewCarsInventoryViewModel>(cachedModelAsBytes);
                var cachedModel = Mapper.Map<CarsInventoryViewModel>(modelAsUsedViewModel);
                return View(cachedModel);
            }


            var sortStrategy = UsedCarSortStrategyFactory.GetStrategy<UsedCar>(sortType, sortDirection);

            var priceRanges = ParameterParser.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series);

            var filteredCars = this.usedCarRepository
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var filterMultipleStrategy = CarMultipleFilterStrategyFactory.GetStrategy(model.ModelTypes);
            var filteredMultipleCars = filterMultipleStrategy.Filter(sortedAndFilteredCars);

            var currentPageCarModels = await this.carsService
                .GetCarScheduleViewModelAsync<CarInventoryConciseViewModel>(filteredCars, this.User, model.PageNumber);
            var filterModel = await this.carsFilterTypesService.GetCarFilterModel(sortedAndFilteredCars, filteredMultipleCars);
            var viewModel = new CarsInventoryViewModel()
            {
                SortStrategyType = sortType,
                SortStrategyDirection = sortDirection,
                Cars = currentPageCarModels,
                CurrentPage = model.PageNumber,
                FilterModel = filterModel,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredMultipleCars),
                TotalCarsCount = await filteredMultipleCars.CountAsync()
            };

            //this.carsInventoryService.SelectModelFilterItems(viewModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);

            var serielizedModelAsBytes = JSonHelper.Serialize(viewModel);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.MaxValue
            };
            await this.cache.SetAsync(key, serielizedModelAsBytes, options);

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