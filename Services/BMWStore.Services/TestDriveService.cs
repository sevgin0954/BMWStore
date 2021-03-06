﻿using AutoMapper;
using BMWStore.Common.Constants;
using Enums = BMWStore.Common.Enums;
using BMWStore.Common.Validation;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.Models;

namespace BMWStore.Services
{
    public class TestDriveService : ITestDriveService
    {
        private readonly ITestDriveRepository testDriveRepository;
        private readonly IStatusRepository statusRepository;
        private readonly ICarRepository carRepository;
        private readonly UserManager<User> userManager;

        public TestDriveService(
            ITestDriveRepository testDriveRepository, 
            IStatusRepository statusRepository,
            ICarRepository carRepository,
            UserManager<User> userManager)
        {
            this.testDriveRepository = testDriveRepository;
            this.statusRepository = statusRepository;
            this.carRepository = carRepository;
            this.userManager = userManager;
        }

        public async Task CancelTestDriveAsync(string testDriveId, ClaimsPrincipal user)
        {
            var dbTestDrive = await this.testDriveRepository.GetByIdAsync(testDriveId);
            DataValidator.ValidateNotNull(dbTestDrive, new ArgumentException(ErrorConstants.IncorrectId));
            this.ValidateTestDriveUser(dbTestDrive, user);
            await this.ValidateStatusAsync(dbTestDrive.StatusId);

            var dbCanceledStatusId = await this.statusRepository
                .GetIdByNameAsync(Enums.TestDriveStatus.Canceled.ToString());
            dbTestDrive.StatusId = dbCanceledStatusId;

            var rowsAffected = await this.testDriveRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        private void ValidateTestDriveUser(TestDrive dbTestDrive, ClaimsPrincipal user)
        {
            var dbUserId = this.userManager.GetUserId(user);
            if (dbTestDrive.UserId != dbUserId)
            {
                throw new ArgumentException(ErrorConstants.IncorrectId);
            }
        }

        private async Task ValidateStatusAsync(string statusId)
        {
            var dbUpcomingStatusId = await this.statusRepository
                .GetIdByNameAsync(Enums.TestDriveStatus.Upcoming.ToString());
            if (statusId != dbUpcomingStatusId)
            {
                throw new ArgumentException(ErrorConstants.UpcomingStatusRequired);
            }
        }

        public IQueryable<TestDriveServiceModel> GetAll(ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            DataValidator.ValidateNotNull(userId, new InvalidOperationException(ErrorConstants.NotSignIn));

            var models = this.testDriveRepository
                .Find(td => td.UserId == userId)
                .To<TestDriveServiceModel>();

            return models;
        }

        public async Task<TestDriveServiceModel> GetByIdAsync(string testDriveId, ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            DataValidator.ValidateNotNull(userId, new InvalidOperationException(ErrorConstants.NotSignIn));

            var model = await this.testDriveRepository
                .Find(td => td.Id == testDriveId && td.UserId == userId)
                .To<TestDriveServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task<string> ScheduleTestDriveAsync(ScheduleTestDriveServiceModel model, ClaimsPrincipal user)
        {
            await this.ValidateCarIdAsync(model.CarId);

            var dbTestDrive = Mapper.Map<TestDrive>(model);

            var dbStatusId = await this.statusRepository
                .Find(s => s.Name == Enums.TestDriveStatus.Upcoming.ToString())
                .Select(s => s.Id)
                .FirstAsync();

            var userId = this.userManager.GetUserId(user);
            DataValidator.ValidateNotNull(userId, new InvalidOperationException(ErrorConstants.NotSignIn));

            dbTestDrive.StatusId = dbStatusId;
            dbTestDrive.UserId = userId;

            this.testDriveRepository.Add(dbTestDrive);

            var rowsAffected = await this.testDriveRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);

            return dbTestDrive.Id;
        }

        private async Task ValidateCarIdAsync(string carId)
        {
            var isCarExist = await this.carRepository.AnyAsync(c => c.Id == carId);
            if (isCarExist == false)
            {
                throw new ArgumentException(ErrorConstants.IncorrectId);
            }
        }
    }
}