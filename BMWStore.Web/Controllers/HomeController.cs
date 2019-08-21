using BMWStore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.homeService.GetHomeModelAsync();
            model.TargetUrlsPublicIds.Add(
                "/Car?carId=8de3b45a-b5a0-4e62-9fb9-47782285ba48", 
                "car_images/jpeelgxqaiq6beugayku");

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
