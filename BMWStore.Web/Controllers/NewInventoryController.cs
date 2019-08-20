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
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInventoryController : Controller
    {
        private readonly IDistributedCache cache;
        private readonly ICarsInventoryService carsInventoryService;
        private readonly ICookiesService cookiesService;
        private readonly INewCarRepository newCarRepository;

        public NewInventoryController(
            IDistributedCache cache,
            ICarsInventoryService carsInventoryService,
            ICookiesService cookiesService,
            INewCarRepository newCarRepository)
        {
            this.cache = cache;
            this.carsInventoryService = carsInventoryService;
            this.cookiesService = cookiesService;
            this.newCarRepository = newCarRepository;
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
                model.Year,
                sortDirection.ToString(),
                sortType.ToString());

            var cachedModelAsBytes = await this.cache.GetAsync(key);
            if (cachedModelAsBytes != null)
            {
                var modelAsNewViewModel = JSonHelper.Desirialize<NewCarsInventoryViewModel>(cachedModelAsBytes);
                var cachedModel = Mapper.Map<CarsInventoryViewModel>(modelAsNewViewModel);
                return View(cachedModel);
            }

            var sortStrategy = NewCarSortStrategyFactory.GetStrategy<NewCar>(sortType, sortDirection);

            var priceRanges = ParameterParser.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series);

            var filteredCars = this.newCarRepository
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var filterStrategy = CarMultipleFilterStrategyFactory.GetStrategy(model.ModelTypes);
            var viewModel = await this.carsInventoryService
                .GetInventoryViewModelAsync(filterStrategy, sortedAndFilteredCars, this.User, model.PageNumber);
            this.carsInventoryService.SelectModelFilterItems(viewModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);

            viewModel.SortStrategyDirection = sortDirection;
            viewModel.SortStrategyType = sortType;

            var serielizedModelAsBytes = JSonHelper.Serialize(viewModel);
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.MaxValue
            };
            await this.cache.SetAsync(key, serielizedModelAsBytes, options);

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