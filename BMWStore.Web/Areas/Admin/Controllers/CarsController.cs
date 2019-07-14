using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarsController : BaseAdminController
    {
        private readonly IAdminCarsService adminCarsService;
        private readonly IAdminSortCookieService adminSortCookieService;

        public CarsController(IAdminCarsService adminCarsService, IAdminSortCookieService adminSortCookieService)
        {
            this.adminCarsService = adminCarsService;
            this.adminSortCookieService = adminSortCookieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await this.adminCarsService.GetAllCarsAsync();
            var cookies = this.HttpContext.Request.Cookies;
            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            var sortStrategyType = this.adminSortCookieService
                .GetSortStrategyTypeOrDefault<CarSortStrategyType>(cookies, sortTypeKey);
            var model = new AdminCarsViewModel()
            {
                Cars = cars,
                SortStrategyDirection = this.adminSortCookieService
                    .GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey),
                SortStrategyType = sortStrategyType
            };

            return View(model);
        }
    }
}