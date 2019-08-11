using BMWStore.Entities;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarOptionsController : BaseAdminController
    {
        private readonly IAdminOptionsService adminCarOptionsService;
        private readonly IAdminDeleteService adminDeleteService;

        public CarOptionsController(IAdminOptionsService adminCarOptionsService, IAdminDeleteService adminDeleteService)
        {
            this.adminCarOptionsService = adminCarOptionsService;
            this.adminDeleteService = adminDeleteService;
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
            await this.adminDeleteService.DeleteAsync<Option>(id);

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
            await this.adminCarOptionsService.EditOptionAsync(model);

            return RedirectToAction("Index");
        }
    }
}