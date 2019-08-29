using Microsoft.AspNetCore.Mvc;

namespace BMWStore.Web.Controllers
{
    public class ContantsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}