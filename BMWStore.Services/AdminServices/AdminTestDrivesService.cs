using BMWStore.Models.TestDriveModels.ViewModels;
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
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Common.Helpers;

namespace BMWStore.Services.AdminServices
{
    public class AdminTestDrivesService : IAdminTestDrivesService
    {
        private readonly ITestDriveRepository testDriveRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminTestDrivesService(
            ITestDriveRepository testDriveRepository, 
            IStatusRepository statusRepository,
            IAdminDeleteService adminDeleteService)
        {
            this.testDriveRepository = testDriveRepository;
            this.statusRepository = statusRepository;
            this.adminDeleteService = adminDeleteService;
        }

        public async Task<AdminTestDrivesViewModel> GetTestDrivesViewModelAsync(
            AdminTestDrivesSortStrategyType sortType,
            SortStrategyDirection sortDirection,
            int pageNumber)
        {
            var sortStrategy = TestDriveSortStrategyFactory.GetStrategy(sortType, sortDirection);
            var sortedTestDrives = sortStrategy.Sort(this.testDriveRepository.GetAll());

            var testDriveModels = await sortedTestDrives
                .GetFromPage(pageNumber)
                .To<TestDriveViewModel>()
                .ToArrayAsync();

            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(sortedTestDrives);

            var model = new AdminTestDrivesViewModel()
            {
                TestDrives = testDriveModels,
                SortDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return model;
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
            await this.adminDeleteService.DeleteAsync<TestDrive>(testDriveId);
        }
    }
}
