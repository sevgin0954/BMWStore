using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
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
        private readonly IReadService readService;

        public AdminUsersService(
            UserManager<User> userManager,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IReadService readService)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.readService = readService;
        }

        public async Task<TModel> GetUserByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, User>(id);

            return model;
        }

        public async Task<IQueryable<UserServiceModel>> GetSortedUsersAsync(
            IUserSortStrategy sortStrategy, 
            int pageNumber)
        {
            var dbUserRoleId = await this.roleRepository
                .GetIdByNameAsync(WebConstants.UserRoleName);

            var sortedUserModels = this.userRepository.GetSortedWithRole(sortStrategy, dbUserRoleId)
                .To<UserServiceModel>()
                .GetFromPage(pageNumber);

            return sortedUserModels;
        }

        public async Task BanUserAsync(string userId)
        {
            var dbUser = await this.GetValidatedUser(userId);
            if (this.IsUserBanned(dbUser))
            {
                throw new ArgumentException(ErrorConstants.IncorrectUser);
            }

            dbUser.LockoutEnd = DateTimeOffset.UtcNow.AddDays(WebConstants.UserBanDays);

            var rowsAffected = await userRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task UnbanUserAsync(string userId)
        {
            var dbUser = await this.GetValidatedUser(userId);
            if (this.IsUserBanned(dbUser) == false)
            {
                throw new ArgumentException(ErrorConstants.IncorrectUser);
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

        private bool IsUserBanned(User user)
        {
            if (user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                return true;
            }

            return false;
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

        private async Task ValidateUserRoleAsync(User user)
        {
            if (await this.userManager.IsInRoleAsync(user, WebConstants.UserRoleName) == false)
            {
                throw new ArgumentException(ErrorConstants.IncorrectUser);
            }
        }
    }
}
