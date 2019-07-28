using BMWStore.Data.Interfaces;
using BMWStore.Models.TestDriveModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Common.Enums;
using System.Linq;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;

namespace BMWStore.Services.AdminServices
{
    public class AdminTestDrivesService : IAdminTestDrivesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminTestDrivesService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync(ITestDriveSortStrategy sortStrategy)
        {
            var models = await sortStrategy.Sort(this.unitOfWork.TestDrives.GetAll())
                .To<TestDriveViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task CheckTestDriveStatusAsync(string testDriveId)
        {
            var dbTestDrive = await this.unitOfWork.TestDrives.GetByIdAsync(testDriveId);
            DataValidator.ValidateNotNull(dbTestDrive, new ArgumentException(ErrorConstants.IncorrectId));

            var dbPassedStatusId = await this.unitOfWork.Statuses
                .Find(s => s.Name == TestDriveStatus.Passed.ToString())
                .Select(s => s.Id)
                .FirstAsync();
            DataValidator.ValidateNotNull(dbPassedStatusId, new Exception(ErrorConstants.StatusNotFound));

            dbTestDrive.StatusId = dbPassedStatusId;

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string testDriveId)
        {
            var dbTestDrive = await this.unitOfWork.TestDrives.GetByIdAsync(testDriveId);
            DataValidator.ValidateNotNull(dbTestDrive, new ArgumentException(ErrorConstants.IncorrectId));

            this.unitOfWork.TestDrives.Remove(dbTestDrive);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
