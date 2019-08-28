using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        private const int CacheTimeOutMinutes = 5;

        private readonly IAdminDashboardStatisticsService adminDashboardStatisticsService;
        private readonly ICacheService cacheService;

        public DashboardController(
            IAdminDashboardStatisticsService adminDashboardStatisticsService,
            ICacheService cacheService)
        {
            this.adminDashboardStatisticsService = adminDashboardStatisticsService;
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cacheKey = KeyGenerator.Generate(WebConstants.CacheDashboardStatisticsPrepend);
            var cachedViewModel = await this.cacheService.GetOrDefaultAsync<AdminDashboardStatisticsViewModel>(cacheKey);
            if (cachedViewModel != null)
            {
                return View(cachedViewModel);
            }

            var serviceModel = await this.adminDashboardStatisticsService.GetStatisticsAsync();
            var viewModel = Mapper.Map<AdminDashboardStatisticsViewModel>(serviceModel);

            var cacheExpireTime = DateTime.UtcNow.AddMinutes(CacheTimeOutMinutes);
            _ = this.cacheService.AddTimedCacheAsync(viewModel, cacheKey, WebConstants.CacheStatisticsType, cacheExpireTime);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCache()
        {
            await this.cacheService.RemoveAsync(WebConstants.CacheStatisticsType);

            return RedirectToAction("Index");
        }
    }
}