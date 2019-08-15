using BMWStore.Common.Constants;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TransmissionController : BaseAdminController
    {
        private readonly IAdminTransmissionsService transmissionsService;

        public TransmissionController(IAdminTransmissionsService transmissionsService)
        {
            this.transmissionsService = transmissionsService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await this.transmissionsService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new TransmissionCreateBindingModel();

            return View(model);
        }

        // TODO: Add Filter for model validation
        [HttpPost]
        public async Task<IActionResult> AddNew(TransmissionCreateBindingModel model)
        {
            await this.transmissionsService.CreateNewTransmissionAsync(model);

            return Redirect(WebConstants.AdminCreateNewEngineUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.transmissionsService.GetEditingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransmissionEditBindingModel model)
        {
            await this.transmissionsService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.transmissionsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}