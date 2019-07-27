using BMWStore.Data.Interfaces;
using Enums = BMWStore.Common.Enums;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using BMWStore.Entities;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;

namespace BMWStore.Services.AdminServices
{
    public class AdminTestDrivesService : IAdminTestDrivesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public AdminTestDrivesService(IBMWStoreUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync()
        {
            var models = await this.unitOfWork.TestDrives
                .GetAll()
                .To<TestDriveViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task ChangeTestDriveStatusAsync(
            Enums.TestDriveStatus newStatus, 
            string testDriveId,
            ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var dbTestDriveStatus = await this.unitOfWork.TestDrives
                .GetByIdAsync(testDriveId);

            // TODO: Create index for status.Name
            var dbStatus = await this.unitOfWork.Statuses
                .Find(tds => tds.Name == newStatus.ToString())
                .FirstAsync();

            if (dbTestDriveStatus.StatusId == dbStatus.Id)
            {
                throw new ArgumentException(ErrorConstants.IncorrectParameterValue);
            }

            dbTestDriveStatus.Status = dbStatus;

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
