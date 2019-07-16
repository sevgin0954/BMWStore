using BMWStore.Common.Constants;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TransmissionController : BaseAdminController
    {
        private readonly ITransmissionsService transmissionsService;

        public TransmissionController(ITransmissionsService transmissionsService)
        {
            this.transmissionsService = transmissionsService;
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new AdminTransmissionsCreateBindingModel();

            return View(model);
        }

        // TODO: Add Filter for model validation
        [HttpPost]
        public async Task<IActionResult> AddNew(AdminTransmissionsCreateBindingModel model)
        {
            await this.transmissionsService.CreateNewTransmissionAsync(model);

            return Redirect(WebConstants.AdminCreateNewEngineUrl);
        }
    }
}