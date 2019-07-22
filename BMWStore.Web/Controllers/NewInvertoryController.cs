using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInvertoryController : Controller
    {
        private readonly INewCarsService newCarsService;

        public NewInvertoryController(INewCarsService newCarsService)
        {
            this.newCarsService = newCarsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.newCarsService.GetAllAsync();

            return View(models);
        }
    }
}