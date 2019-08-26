using AutoMapper;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        private readonly IAdminDashboardStatisticsService adminDashboardStatisticsService;

        public DashboardController(IAdminDashboardStatisticsService adminDashboardStatisticsService)
        {
            this.adminDashboardStatisticsService = adminDashboardStatisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceModel = await this.adminDashboardStatisticsService.GetStatisticsAsync();
            var viewModel = Mapper.Map<AdminDashboardStatisticsViewModel>(serviceModel);

            return View(viewModel);
        }
    }
}