using BMWStore.Common.Constants;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Entities;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionsController : BaseAdminController
    {
        private readonly IAdminOptionsService adminOptionsService;
        private readonly ISelectListItemsService selectListItemsService;
        private readonly ICookiesService cookiesService;

        public OptionsController(
            IAdminOptionsService adminOptionsService,
            ISelectListItemsService selectListItemsService,
            ICookiesService cookiesService)
        {
            this.adminOptionsService = adminOptionsService;
            this.selectListItemsService = selectListItemsService;
            this.cookiesService = cookiesService;
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

            var filterStrategy = OptionFilterStrategyFactory.GetStrategy(filter, name);
            var model = await this.adminOptionsService.GetOptionsViewModelAsync(
                filterStrategy,
                sortType,
                sortDirection,
                pageNumber);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var optionTypeModels = await this.selectListItemsService.GetAllAsSelectListItemsAsync<OptionType>();
            var model = new OptionBindingModel()
            {
                OptionTypes = optionTypeModels
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionBindingModel model)
        {
            await this.adminOptionsService.CreateNewOptionAsync(model);

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
            var model = await this.adminOptionsService.GetEditBindingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OptionBindingModel model)
        {
            await this.adminOptionsService.EditOptionAsync(model);

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