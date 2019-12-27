using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Web.Factories.FilterStrategyFactory;
using BMWStore.Web.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMWStore.Models.SearchModels.BindingModels;

namespace BMWStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICarsService carsService;
        private readonly ICookiesService cookiesService;
		private readonly ICarTestDriveService carTestDriveService;

		public SearchController(
			ICarsService carsService,
			ICookiesService cookiesService,
			ICarTestDriveService carTestDriveService)
        {
            this.carsService = carsService;
            this.cookiesService = cookiesService;
			this.carTestDriveService = carTestDriveService;
		}

        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserSearchCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserSearchCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<BaseCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = BaseCarSortStrategyFactory
                .GetStrategy<BaseCar>(sortType, sortDirection);

            var splitedKeyWords = ParameterParser
                .ParseSearchKeyWordsParameter(model.KeyWords, WebConstants.MinSearchKeyWordLength)
                .Distinct()
                .ToArray();
            var filterStrategies = CarSearchFilterStrategyFactory.GetStrategies(splitedKeyWords);

            IEnumerable<CarInventoryConciseViewModel> carViewModels = new List<CarInventoryConciseViewModel>();
            int totalPagesCount = 0;
            if (filterStrategies.Count > 0)
            {
                var filteredCars = this.carsService.GetFiltered<BaseCar>(filterStrategies.ToArray());
                var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

                carViewModels = await (await this.carTestDriveService
					.GetCarTestDriveModelAsync<CarConciseTestDriveServiceModel>(filteredAndSortedCars, this.User, model.PageNumber))
                    .To<CarInventoryConciseViewModel>()
                    .ToArrayAsync();

                totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredCars);
            }

            var searchModel = new CarSearchViewModel()
            {
                Cars = carViewModels,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = model.PageNumber,
                TotalPagesCount = totalPagesCount,
                KeyWords = splitedKeyWords
            };

            return View(searchModel);
        }

        [HttpPost]
        public IActionResult ChangeSortType(BaseCarSortStrategyType sortStrategyType, string returnUrl)
        {
            var sortTypeKey = WebConstants.CookieUserSearchCarsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection, string returnUrl)
        {
            var sortDirectionKey = WebConstants.CookieUserSearchCarsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return Redirect(returnUrl);
        }
    }
}