using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionTypesController : BaseAdminController
    {
        private readonly IAdminOptionTypesService adminOptionTypesService;
        private readonly IReadService readService;

        public OptionTypesController(IAdminOptionTypesService adminOptionTypesService, IReadService readService)
        {
            this.adminOptionTypesService = adminOptionTypesService;
            this.readService = readService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new OptionTypeCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionTypeCreateBindingModel model)
        {
            await this.adminOptionTypesService.CreateOptionTypeAsync(model);

            return RedirectToAction("Index");
        }
    }
}