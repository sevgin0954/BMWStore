using BMWStore.Common.Constants;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class FuelTypeController : BaseAdminController
    {
        private readonly IFuelTypesService fuelTypesService;

        public FuelTypeController(IFuelTypesService fuelTypesService)
        {
            this.fuelTypesService = fuelTypesService;
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

            // TODO: Move to constant
            return Redirect(WebConstants.AdminCreateNewCarUrl);
        }
    }
}