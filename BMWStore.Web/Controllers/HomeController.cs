using BMWStore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;
using BMWStore.Models.HomeModels.ViewModels;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using System.Linq;

namespace BMWStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly ICarRepository carRepository;

        public HomeController(IHomeService homeService, ICarRepository carRepository)
        {
            this.homeService = homeService;
            this.carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            var newCars = this.carRepository.Set<NewCar>().AsQueryable();

            var searchModel = await this.homeService.GetSearchModelAsync(newCars);
            var model = new HomeViewModel();
            model.SearchModel = searchModel;
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
