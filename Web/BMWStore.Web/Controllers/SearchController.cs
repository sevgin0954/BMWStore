﻿using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
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

namespace BMWStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICarsService carsService;
        private readonly ICarRepository carRepository;
        private readonly ICookiesService cookiesService;

        public SearchController(ICarsService carsService, ICarRepository carRepository, ICookiesService cookiesService)
        {
            this.carsService = carsService;
            this.carRepository = carRepository;
            this.cookiesService = cookiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyWords = "", int pageNumber = 1)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserSearchCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserSearchCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<BaseCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = BaseCarSortStrategyFactory
                .GetStrategy<BaseCar>(sortType, sortDirection);

            var splitedKeyWords = ParameterParser
                .ParseSearchKeyWordsParameter(keyWords, WebConstants.MinSearchKeyWordLength)
                .Distinct()
                .ToArray();
            var filterStrategies = CarSearchFilterStrategyFactory.GetStrategies(splitedKeyWords);

            IEnumerable<CarInventoryConciseViewModel> carViewModels = new List<CarInventoryConciseViewModel>();
            int totalPagesCount = 0;
            if (filterStrategies.Count > 0)
            {
                var filteredCars = this.carRepository.GetFiltered(filterStrategies.ToArray());
                var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

                carViewModels = await this.carsService
                    .GetCarTestDriveModel<CarConciseTestDriveServiceModel>(filteredAndSortedCars, this.User, pageNumber)
                    .To<CarInventoryConciseViewModel>()
                    .ToArrayAsync();

                totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredCars);
            }

            var model = new CarSearchViewModel()
            {
                Cars = carViewModels,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount,
                KeyWords = splitedKeyWords
            };

            return View(model);
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