using AutoMapper;
using BMWStore.Common.Constants;
using Enums = BMWStore.Common.Enums;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.TestDriveModels.BindingModels;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync(ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var model = await this.unitOfWork.TestDrives
                .Find(td => td.UserId == userId)
                .To<TestDriveViewModel>()
                .ToArrayAsync();

            return model;
        }

        public async Task<TestDriveViewModel> GetTestDriveAsync(string testDriveId, ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var model = await this.unitOfWork.TestDrives
                .Find(td => td.Id == testDriveId && td.UserId == userId)
                .To<TestDriveViewModel>()
                .FirstAsync();

            return model;
        }

        public async Task<string> ScheduleTestDriveAsync(ScheduleTestDriveBindingModel model, ClaimsPrincipal user)
        {
            var dbTestDrive = Mapper.Map<TestDrive>(model);
            var dbStatusId = await this.unitOfWork.Statuses
                .Find(s => s.Name == Enums.TestDriveStatus.Upcoming.ToString())
                .Select(s => s.Id)
                .FirstAsync();
            var userId = this.userManager.GetUserId(user);
            dbTestDrive.StatusId = dbStatusId;
            dbTestDrive.UserId = userId;

            this.unitOfWork.TestDrives.Add(dbTestDrive);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);

            return dbTestDrive.Id;
        }

        // TODO: Refactor
        public async Task<IDictionary<string, string>> GetCarIdTestDriveIdKvpAsync(
            string userId, 
            Expression<Func<TestDrive, bool>> predicate)
        {
            var kvp = await this.unitOfWork.TestDrives
                .Find(predicate)
                .Where(td => td.UserId == userId)
                .Select(td => new KeyValuePair<string, string>(td.CarId, td.Id))
                .ToArrayAsync();

            var result = new Dictionary<string, string>(kvp);
            return result;
        }

        public async Task CancelTestDriveAsync(string testDriveId, ClaimsPrincipal user)
        {
            var dbTestDrive = await this.unitOfWork.TestDrives.GetByIdAsync(testDriveId);

            // TODO: Create index for status.Name
            var dbCanceledStatus = await this.unitOfWork.Statuses
                .Find(tds => tds.Name == Enums.TestDriveStatus.Canceled.ToString())
                .FirstAsync();

            if (dbTestDrive.StatusId == dbCanceledStatus.Id)
            {
                throw new ArgumentException(ErrorConstants.IncorrectParameterValue);
            }

            dbTestDrive.Status = dbCanceledStatus;

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
