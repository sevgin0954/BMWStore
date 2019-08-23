using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Common.Validation;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.UserModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly ICookiesService cookiesService;
        private readonly IReadService readService;

        public AdminUsersService(
            UserManager<User> userManager,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ICookiesService cookiesService,
            IReadService readService)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.cookiesService = cookiesService;
            this.readService = readService;
        }

        public async Task<UserAdminViewModel> GetUserByIdAsync(string id)
        {
            var model = await this.readService.GetModelByIdAsync<UserAdminViewModel, User>(id);

            return model;
        }

        public async Task<AdminUsersViewModel> GetSortedUsersAsync(
            IRequestCookieCollection requestCookies, 
            int pageNumber)
        {
            var sortStrategyName = this.cookiesService
                .GetValueOrDefault<UserSortStrategyType>(requestCookies, WebConstants.CookieAdminUsersSortTypeKey);
            var sortDirection = this.cookiesService
                .GetValueOrDefault<SortStrategyDirection>(requestCookies, WebConstants.CookieAdminUsersSortDirectionKey);
            var sortStrategy = UserSortStrategyFactory.GetStrategy(sortStrategyName, sortDirection);

            var dbUserRoleId = await this.roleRepository
                .GetIdByNameAsync(WebConstants.UserRoleName);

            var sortedUsers = this.userRepository
                .GetSortedWithRole(sortStrategy, dbUserRoleId);

            var userModels = await this.readService.GetAllAsync<UserAdminViewModel, User>(sortedUsers, pageNumber);

            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(sortedUsers);

            var model = new AdminUsersViewModel()
            {
                Users = userModels,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortStrategyName,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return model;
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
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
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
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
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

        public async Task DeleteUserAsync(string userId)
        {
            var dbUser = await this.userRepository.GetByIdAsync(userId);
            DataValidator.ValidateNotNull(dbUser, new ArgumentException(ErrorConstants.IncorrectId));
            await this.ValidateUserRoleAsync(dbUser);

            this.userRepository.Remove(dbUser);

            var rowsAffected = await userRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
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
