﻿using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.UserModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public AdminUsersService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync(IUserSortStrategy sortStrategy)
        {
            var dbUserRoleId = await this.unitOfWork.Roles
                .GetIdByNameAsync(WebConstants.UserRoleName);
            var dbUsers = this.unitOfWork.Users
                .GetSortedWithRole(sortStrategy, dbUserRoleId);
            // TODO: Make use of automapper ProjectTo
            var models = Mapper.Map<IEnumerable<UserAdminViewModel>>(dbUsers);

            return models;
        }

        public async Task BanUserAsync(string userId)
        {
            var dbUser = await this.unitOfWork.Users.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));

            if (this.IsUserBanned(dbUser))
            {
                throw new ArgumentException(ErrorConstants.UserIsAlreadyBanned);
            }

            dbUser.LockoutEnd = DateTimeOffset.UtcNow.AddDays(WebConstants.UserBanDays);

            await this.CompleteUnitOfWork();
        }

        public async Task UnbanUserAsync(string userId)
        {
            var dbUser = await this.unitOfWork.Users.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));

            if (this.IsUserBanned(dbUser) == false)
            {
                throw new ArgumentException(ErrorConstants.UserIsNotBanned);
            }

            dbUser.LockoutEnd = null;

            await this.CompleteUnitOfWork();
        }

        private bool IsUserBanned(User user)
        {
            if (user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var dbUser = await this.unitOfWork.Users.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));

            this.unitOfWork.Users.Remove(dbUser);

            await this.CompleteUnitOfWork();
        }

        private async Task CompleteUnitOfWork()
        {
            var rowsAffected = await unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}