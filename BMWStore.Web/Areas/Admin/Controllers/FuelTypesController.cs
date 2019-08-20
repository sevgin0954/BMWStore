using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class FuelTypesController : BaseAdminController
    {
        private readonly IAdminFuelTypesService fuelTypesService;

        public FuelTypesController(IAdminFuelTypesService fuelTypesService)
        {
            this.fuelTypesService = fuelTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var model = await this.fuelTypesService.GetFuelTypesViewModelAsync(pageNumber);

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new FuelTypeBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(FuelTypeBindingModel model)
        {
            await this.fuelTypesService.CreateNewFuelTypeAsync(model);

            return Redirect("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.fuelTypesService.GetEditingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FuelTypeBindingModel model)
        {
            await this.fuelTypesService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.fuelTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}