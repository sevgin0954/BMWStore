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
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.AdminServices
{
    public class AdminTestDrivesService : IAdminTestDrivesService
    {
        private readonly ITestDriveRepository testDriveRepository;
        private readonly IStatusRepository statusRepository;

        public AdminTestDrivesService(ITestDriveRepository testDriveRepository, IStatusRepository statusRepository)
        {
            this.testDriveRepository = testDriveRepository;
            this.statusRepository = statusRepository;
        }

        public async Task<IEnumerable<TestDriveViewModel>> GetAllTestDrivesAsync(ITestDriveSortStrategy sortStrategy)
        {
            var models = await sortStrategy.Sort(this.testDriveRepository.GetAll())
                .To<TestDriveViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task ChangeTestDriveStatusToPassedAsync(string testDriveId)
        {
            var dbTestDrive = await this.testDriveRepository.GetByIdAsync(testDriveId);
            DataValidator.ValidateNotNull(dbTestDrive, new ArgumentException(ErrorConstants.IncorrectId));
            await this.ValidateTestDriveStatus(dbTestDrive);

            var dbPassedStatusId = await this.statusRepository
                .Find(s => s.Name == TestDriveStatus.Passed.ToString())
                .Select(s => s.Id)
                .FirstAsync();
            dbTestDrive.StatusId = dbPassedStatusId;

            var rowsAffected = await this.testDriveRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task ValidateTestDriveStatus(TestDrive dbTestDrive)
        {
            var dbUpcomingStatusId = await this.statusRepository
                .Find(s => s.Name == TestDriveStatus.Upcoming.ToString())
                .Select(s => s.Id)
                .FirstAsync();

            if (dbTestDrive.StatusId != dbUpcomingStatusId)
            {
                throw new InvalidOperationException(ErrorConstants.StatusIsNotUpcoming);
            }
        }

        public async Task DeleteAsync(string testDriveId)
        {
            var dbTestDrive = await this.testDriveRepository.GetByIdAsync(testDriveId);
            DataValidator.ValidateNotNull(dbTestDrive, new ArgumentException(ErrorConstants.IncorrectId));

            this.testDriveRepository.Remove(dbTestDrive);

            var rowsAffected = await this.testDriveRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
