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
using BMWStore.Data.Interfaces;

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
        private readonly IBMWStoreUnitOfWork unitOfWork;

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
            Enum sortStrategy,
            Enums.SortStrategyDirection sortDirection,
            ClaimsPrincipal user)
        {
            var carModels = await cars.To<CarConciseViewModel>().ToArrayAsync();

            // TODO: Use caching
            var yearModels = await this.carYearService.GetYearFilterModels(cars);
            var seriesModels = await this.carSeriesService.GetSeriesFilterModels(cars);
            var modelTypeModels = await this.carModelTypeService.GetModelTypeFilterModels(cars);
            var priceModels = await this.carPriceService.GetPriceFilterModels(carModels);

            var model = new CarsInvertoryViewModel();

            var allFilterModel = this.AddAllFilterTypeModel();
            model.Years.Add(allFilterModel);
            model.Series.Add(allFilterModel);
            model.Prices.Add(allFilterModel);

            model.Years.AddRange(yearModels);
            model.Series.AddRange(seriesModels);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);

            model.Cars = carModels;

            model.SortStrategyType = sortStrategy;
            model.SortStrategyDirection = sortDirection;

            if (this.signInManager.IsSignedIn(user))
            {
                var userId = this.userManager.GetUserId(user);
                var kvp = await this.testDriveService
                    .GetCarIdTestDriveIdKvpAsync(userId, td => td.Status.Name == Enums.TestDriveStatus.Upcoming.ToString());
                model.CarIdUpcomingTestDriveId = kvp;
            }

            return model;
        }

        private FilterTypeBindingModel AddAllFilterTypeModel()
        {
            var allModel = new FilterTypeBindingModel()
            {
                Text = WebConstants.AllFilterTypeModelText,
                Value = WebConstants.AllFilterTypeModelValue
            };

            return allModel;
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
