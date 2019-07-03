using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class NewInvertoryController : Controller
    {
        private readonly INewCarsInvertoryService newCarsInvertoryService;

        public NewInvertoryController(INewCarsInvertoryService newCarsInvertoryService)
        {
            this.newCarsInvertoryService = newCarsInvertoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.newCarsInvertoryService.GetAllAsync();

            return View(models);
        }
    }
}