using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class TestDriveService : ITestDriveService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public TestDriveService(IBMWStoreUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<TestDriveViewModel> GetTestDriveViewModelAsync(string carId)
        {
            var model = await this.unitOfWork.TestDrives
                .Find(td => td.CarId == carId)
                .To<TestDriveViewModel>()
                .FirstAsync();

            return model;
        }

        public async Task ScheduleTestDriveAsync(ScheduleTestDriveBindingModel model, ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var dbTestDrive = Mapper.Map<TestDrive>(model);
            dbTestDrive.UserId = userId;

            this.unitOfWork.TestDrives.Add(dbTestDrive);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<string>> GetAllTestDrivesCarIdsAsync(ClaimsPrincipal user)
        {
            var usedId = this.userManager.GetUserId(user);
            var ids = await this.unitOfWork.TestDrives
                .Find(td => td.UserId == usedId)
                .Select(td => td.CarId)
                .ToArrayAsync();

            return ids;
        }
    }
}
