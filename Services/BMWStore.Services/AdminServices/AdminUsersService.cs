using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;

        public AdminUsersService(
            UserManager<User> userManager,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        public async Task BanUserAsync(string userId)
        {
            var dbUser = await this.GetValidatedUser(userId);
            if (this.IsUserBanned(dbUser))
            {
                throw new InvalidOperationException(ErrorConstants.IncorrectUser);
            }

            dbUser.LockoutEnd = DateTimeOffset.UtcNow.AddDays(WebConstants.UserBanDays);

            var rowsAffected = await userRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string userId)
        {
            var dbUser = await this.userRepository.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));
            await this.ValidateUserRoleAsync(dbUser);

            this.userRepository.Remove(dbUser);

            var rowsAffected = await userRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task<IQueryable<UserConciseServiceModel>> GetSortedUsersAsync(
            IUserSortStrategy sortStrategy,
            int pageNumber)
        {
            var dbUserRoleId = await this.roleRepository
                .GetIdByNameAsync(WebConstants.UserRoleName);

            var dbUsers = this.userRepository.Find(u => u.Roles.Any(r => r.RoleId == dbUserRoleId));

            var sortedUserModels = sortStrategy.Sort(dbUsers)
                .GetFromPage(pageNumber, WebConstants.PageSize)
                .To<UserConciseServiceModel>();

            return sortedUserModels;
        }

        public async Task<UserConciseServiceModel> GetUserByIdAsync(string id)
        {
            var model = await this.userRepository
                .FindAll(id)
                .To<UserConciseServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task UnbanUserAsync(string userId)
        {
            var dbUser = await this.GetValidatedUser(userId);
            if (this.IsUserBanned(dbUser) == false)
            {
                throw new InvalidOperationException(ErrorConstants.IncorrectUser);
            }

            dbUser.LockoutEnd = null;

            var rowsAffected = await userRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        private async Task<User> GetValidatedUser(string userId)
        {
            var dbUser = await this.userRepository.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));
            await this.ValidateUserRoleAsync(dbUser);

            return dbUser;
        }

        private async Task ValidateUserRoleAsync(User user)
        {
            if (await this.userManager.IsInRoleAsync(user, WebConstants.UserRoleName) == false)
            {
                throw new InvalidOperationException(ErrorConstants.IncorrectUser);
            }
        }

        private bool IsUserBanned(User user)
        {
            if (user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                return true;
            }

            return false;
        }
    }
}