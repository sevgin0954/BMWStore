using BMWStore.Common.Enums.FilterStrategies;
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

        public OptionsController(
            IAdminOptionsService adminOptionsService,
            ISelectListItemsService selectListItemsService)
        {
            this.adminOptionsService = adminOptionsService;
            this.selectListItemsService = selectListItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string name,
            AdminOptionFilterStrategy filterType = AdminOptionFilterStrategy.All, 
            int pageNumber = 1)
        {
            var filterStrategy = OptionFilterStrategyFactory.GetStrategy(filterType, name);
            var model = await this.adminOptionsService.GetOptionsViewModelAsync(filterStrategy, pageNumber);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var optionTypeModels = await this.selectListItemsService.GetAllAsSelectListItemsAsync<OptionType>();
            var model = new AdminOptionCreateBindingModel()
            {
                OptionTypes = optionTypeModels
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminOptionCreateBindingModel model)
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
        public async Task<IActionResult> Edit(AdminCarOptionEditBindingModel model)
        {
            await this.adminOptionsService.EditOptionAsync(model);

            return RedirectToAction("Index");
        }
    }
}