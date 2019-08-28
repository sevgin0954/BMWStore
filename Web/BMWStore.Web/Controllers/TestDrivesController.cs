using AutoMapper;
using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    [Authorize]
    public class TestDrivesController : Controller
    {
        private readonly ITestDriveService testDriveService;

        public TestDrivesController(ITestDriveService testDriveService)
        {
            this.testDriveService = testDriveService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModels = await this.testDriveService.GetAll(this.User).ToArrayAsync();
            var viewModels = Mapper.Map<IEnumerable<TestDriveViewModel>>(serviceModels);

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Drive(string testDriveId)
        {
            var serviceModel = await this.testDriveService.GetByIdAsync(testDriveId, this.User);
            var viewModel = Mapper.Map<TestDriveViewModel>(serviceModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleTestDriveBindingModel model)
        {
            var serviceModel = Mapper.Map<ScheduleTestDriveServiceModel>(model);
            var testDriveId = await this.testDriveService.ScheduleTestDriveAsync(serviceModel, this.User);

            return RedirectToAction("Drive", new { testDriveId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string testDriveId)
        {
            await this.testDriveService.CancelTestDriveAsync(testDriveId, this.User);

            return RedirectToAction("Drive", new { testDriveId });
        }
    }
}