using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Web.Factories.FilterStrategyFactory;
using BMWStore.Web.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TestDrivesController : BaseAdminController
    {
        private readonly IAdminTestDrivesService adminTestDrivesService;
        private readonly ICookiesService cookiesService;
        private readonly ITestDriveRepository testDriveRepository;

        public TestDrivesController(
            IAdminTestDrivesService adminTestDrivesService, 
            ICookiesService ookiesService,
            ITestDriveRepository testDriveRepository)
        {
            this.adminTestDrivesService = adminTestDrivesService;
            this.cookiesService = ookiesService;
            this.testDriveRepository = testDriveRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            AdminTestDriveFilterStrategy filter = AdminTestDriveFilterStrategy.All,
            int pageNumber = 1,
            string name = "")
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<AdminTestDrivesSortStrategyType>(cookies, sortTypeKey);

            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);

            var sortStrategy = TestDriveSortStrategyFactory.GetStrategy(sortType, sortDirection);
            var filterStrategy = AdminTestDrivesFilterStrategyFactory.GetStrategy(filter, name);

            var allTestDrives = this.testDriveRepository.GetAll();
            var filteredTestDrives = filterStrategy.Filter(allTestDrives);

            var testDriveViewModels = await this.adminTestDrivesService
                .GetAllSorted(filteredTestDrives, sortStrategy, pageNumber)
                .To<TestDriveViewModel>()
                .ToArrayAsync();
            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(filteredTestDrives);
            var model = new AdminTestDrivesViewModel()
            {
                TestDrives = testDriveViewModels,
                SortDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Check(string testDriveId)
        {
            await this.adminTestDrivesService.ChangeTestDriveStatusToPassedAsync(testDriveId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminTestDrivesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(AdminTestDrivesSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminTestDrivesSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminTestDrivesSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}