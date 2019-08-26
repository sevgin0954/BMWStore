using Microsoft.AspNetCore.Mvc;

namespace BMWStore.Web.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult Index(string search)
        {
            return View();
        }
    }
}