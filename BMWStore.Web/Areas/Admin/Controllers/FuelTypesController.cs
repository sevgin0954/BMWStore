using BMWStore.Entities;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class FuelTypesController : BaseAdminController
    {
        private readonly IAdminFuelTypesService fuelTypesService;
        private readonly IAdminDeleteService adminDeleteService;

        public FuelTypesController(IAdminFuelTypesService fuelTypesService, IAdminDeleteService adminDeleteService)
        {
            this.fuelTypesService = fuelTypesService;
            this.adminDeleteService = adminDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.fuelTypesService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new AdminFuelTypeCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminFuelTypeCreateBindingModel model)
        {
            await this.fuelTypesService.CreateNewFuelTypeAsync(model);

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminDeleteService.DeleteAsync<FuelType>(id);

            return RedirectToAction("Index");
        }
    }
}