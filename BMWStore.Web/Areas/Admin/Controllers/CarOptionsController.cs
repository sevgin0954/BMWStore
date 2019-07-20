using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarOptionsController : BaseAdminController
    {
        private readonly IAdminOptionsService adminCarOptionsService;

        public CarOptionsController(IAdminOptionsService adminCarOptionsService)
        {
            this.adminCarOptionsService = adminCarOptionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.adminCarOptionsService.GetAllOptionsAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new AdminOptionCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminOptionCreateBindingModel model)
        {
            await this.adminCarOptionsService.CreateNewOptionAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminCarOptionsService.DeleteOptionAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.adminCarOptionsService.GetEditBindingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminCarOptionEditBindingModel model)
        {
            await this.adminCarOptionsService.EditOption(model);

            return RedirectToAction("Index");
        }
    }
}