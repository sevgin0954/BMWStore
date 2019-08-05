using BMWStore.Common.Constants;
using Enums = BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Common.Helpers;
using BMWStore.Data.Repositories.Interfaces;

namespace BMWStore.Services
{
    public class CarsInvertoryService : ICarsInvertoryService
    {
        private readonly ICarYearService carYearService;
        private readonly ICarSeriesService carSeriesService;
        private readonly ICarModelTypeService carModelTypeService;
        private readonly ICarPriceService carPriceService;
        private readonly IFilterTypesService filterTypesService;
        private readonly ITestDriveService testDriveService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public CarsInvertoryService(
            ICarYearService carYearService,
            ICarSeriesService carSeriesService,
            ICarModelTypeService carModelTypeService,
            ICarPriceService carPriceService,
            IFilterTypesService filterTypesService,
            ITestDriveService testDriveService,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.carYearService = carYearService;
            this.carSeriesService = carSeriesService;
            this.carModelTypeService = carModelTypeService;
            this.carPriceService = carPriceService;
            this.filterTypesService = filterTypesService;
            this.testDriveService = testDriveService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<CarsInvertoryViewModel> GetInvertoryBindingModel(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
        {
            var totalCarPages = await PaginationHelper.CalculateTotalPagesCount(cars);

            var carModels = await cars
                .GetFromPage(pageNumber)
                .To<CarConciseViewModel>().ToArrayAsync();

            var yearModels = await this.carYearService.GetYearFilterModels(cars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModelsAsync(cars);
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModelsAsync(cars);
            var priceModels = await this.carPriceService.GetPriceFilterModelsAsync(carModels);

            var model = new CarsInvertoryViewModel()
            {
                CurrentPage = pageNumber,
                TotalPagesCount = totalCarPages
            };

            this.AddAllFilterTypeModels(model);

            model.Years.AddRange(yearModels);
            model.Series.AddRange(seriesModels);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);

            model.Cars = carModels;

            if (this.signInManager.IsSignedIn(user))
            {
                var userId = this.userManager.GetUserId(user);
                var kvp = await this.testDriveService
                    .GetCarIdTestDriveIdKvpAsync(userId, td => td.Status.Name == Enums.TestDriveStatus.Upcoming.ToString());
                model.CarIdUpcomingTestDriveId = kvp;
            }

            return model;
        }

        private void AddAllFilterTypeModels(CarsInvertoryViewModel model)
        {
            var allFilterModel = new FilterTypeBindingModel()
            {
                Text = WebConstants.AllFilterTypeModelText,
                Value = WebConstants.AllFilterTypeModelValue
            };

            model.Years.Add(allFilterModel);
            model.Series.Add(allFilterModel);
            model.Prices.Add(allFilterModel);
        }

        public void SelectModelFilterItems(CarsInvertoryViewModel model,
            string year,
            string priceRange,
            string series,
            IEnumerable<string> modelTypes)
        {
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Years, year);
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Series, series);
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.ModelTypes, modelTypes.ToArray());
            this.filterTypesService.SelectFilterTypeModelsWithValues(model.Prices, priceRange);
        }
    }
}
