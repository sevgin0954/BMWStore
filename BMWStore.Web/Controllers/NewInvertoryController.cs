﻿using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInvertoryController : Controller
    {
        private readonly ICarsInvertoryService carsInvertoryService;
        private readonly ICookiesService cookiesService;
        private readonly INewCarRepository newCarRepository;

        public NewInvertoryController(
            ICarsInvertoryService carsInvertoryService,
            ICookiesService cookiesService,
            INewCarRepository newCarRepository)
        {
            this.carsInvertoryService = carsInvertoryService;
            this.cookiesService = cookiesService;
            this.newCarRepository = newCarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarsInvertoryBindingModel model)
        {
            var cookie = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookie, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<BaseCarSortStrategyType>(cookie, sortTypeKey);

            var sortStrategy = NewCarSortStrategyFactory.GetStrategy<NewCar>(sortType, sortDirection);

            var priceRanges = this.ParsePriceRange(model.PriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.Year, priceRanges[0], priceRanges[1], model.Series, model.ModelTypes);

            var filteredCars = this.newCarRepository
                .GetFiltered(filterStrategies.ToArray());
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var viewModel = await this.carsInvertoryService
                .GetInvertoryViewModelAsync(sortedAndFilteredCars, this.User, model.PageNumber);
            this.carsInvertoryService.SelectModelFilterItems(viewModel, model.Year, model.PriceRange, model.Series, model.ModelTypes);
            viewModel.SortStrategyDirection = sortDirection;
            viewModel.SortStrategyType = sortType;

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
        public IActionResult ChangeSortType(BaseCarSortStrategyType sortStrategyType, string returnUrl)
        {
            var sortTypeKey = WebConstants.CookieUserCarsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection, string returnUrl)
        {
            var sortDirectionKey = WebConstants.CookieUserCarsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return Redirect(returnUrl);
        }
    }
}