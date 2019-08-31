using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Common.Enums;
using System.Linq;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Services.Models;

namespace BMWStore.Services.AdminServices
{
    public class AdminTestDrivesService : IAdminTestDrivesService
    {
        private readonly ITestDriveRepository testDriveRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IAdminCommonDeleteService adminDeleteService;

        public AdminTestDrivesService(
            ITestDriveRepository testDriveRepository, 
            IStatusRepository statusRepository,
            IAdminCommonDeleteService adminDeleteService)
        {
            this.testDriveRepository = testDriveRepository;
            this.statusRepository = statusRepository;
            this.adminDeleteService = adminDeleteService;
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
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string testDriveId)
        {
            await this.adminDeleteService.DeleteAsync<TestDrive>(testDriveId);
        }

        public IQueryable<TestDriveServiceModel> GetAllSorted(
            IQueryable<TestDrive> testDrives,
            ITestDriveSortStrategy sortStrategy,
            int pageNumber)
        {
            var sortedTestDrives = sortStrategy.Sort(testDrives);

            var testDriveModels = sortedTestDrives
                .GetFromPage(pageNumber, WebConstants.PageSize)
                .To<TestDriveServiceModel>();

            return testDriveModels;
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
    }
}