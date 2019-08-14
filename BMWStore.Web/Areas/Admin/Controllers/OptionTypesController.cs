using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionTypesController : Controller
    {
        private readonly IAdminOptionTypesService adminOptionTypesService;

        public OptionTypesController(IAdminOptionTypesService adminOptionTypesService)
        {
            this.adminOptionTypesService = adminOptionTypesService;
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