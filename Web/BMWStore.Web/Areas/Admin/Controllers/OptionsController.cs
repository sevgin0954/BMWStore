using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionsController : BaseAdminController
    {
        private readonly IAdminOptionsService adminOptionsService;
        private readonly ICookiesService cookiesService;
        private readonly IOptionRepository optionRepository;

        public OptionsController(
            IAdminOptionsService adminOptionsService,
            ICookiesService cookiesService,
            IOptionRepository optionRepository)
        {
            this.adminOptionsService = adminOptionsService;
            this.cookiesService = cookiesService;
            this.optionRepository = optionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string name,
            AdminOptionFilterStrategy filter = AdminOptionFilterStrategy.All, 
            int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortStrategyKey = WebConstants.CookieAdminOptionsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<OptionSortStrategyType>(cookies, sortStrategyKey);

            var sortDirectionKey = WebConstants.CookieAdminOptionsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);

            var sortStrategy = OptionSortStrategyFactory.GetStrategy(sortType, sortDirection);
            var filterStrategy = OptionFilterStrategyFactory.GetStrategy(filter, name);
            var allOptions = this.optionRepository.GetAll();
            var filteredOptions = filterStrategy.Filter(allOptions);

            var optionModels = await this.adminOptionsService.GetAllSorted(filteredOptions, sortStrategy, pageNumber)
                .To<OptionViewModel>()
                .ToArrayAsync();

            var model = new AdminOptionsViewModel()
            {
                CurrentPage = pageNumber,
                Options = optionModels,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredOptions),
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var optionTypeModels = await this.adminOptionsService.GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();
            var model = new OptionBindingModel()
            {
                OptionTypes = optionTypeModels
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionBindingModel model)
        {
            var serviceModel = Mapper.Map<OptionServiceModel>(model);
            await this.adminOptionsService.CreateNewAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminOptionsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.adminOptionsService.GetByIdAsync<OptionBindingModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OptionBindingModel model)
        {
            var serviceModel = Mapper.Map<OptionServiceModel>(model);
            await this.adminOptionsService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(OptionSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminOptionsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminOptionsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}