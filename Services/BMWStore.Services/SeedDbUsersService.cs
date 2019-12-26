using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SeedDbUsersService : ISeedDbUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly UserManager<User> userManager;

        public SeedDbUsersService(
            IUserRepository userRepository, 
            IRoleRepository roleRepository,
            UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userManager = userManager;
        }

        public async Task SeedUserAsync(UserServiceModel model, string password, string roleName)
        {
            if (await this.IsUserExist(model.Email))
            {
                return;
            }
            if (await this.IsRoleExist(roleName) == false)
            {
                throw new ArgumentException(ErrorConstants.RoleNotFound);
            }

            var dbUser = await this.CreateUserAsync(model, password);
            var result = await this.userManager.AddToRoleAsync(dbUser, roleName);
            DataValidator.ValidateIdentityResult(result);

            var rowsAffected = await this.userRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        private Task<bool> IsUserExist(string email)
        {
            var isExist = this.userRepository.AnyAsync(u => u.NormalizedEmail == email.ToUpper());

            return isExist;
        }

        private async Task<bool> IsRoleExist(string roleName)
        {
            var isExist = await this.roleRepository.AnyAsync(r => r.NormalizedName == roleName.ToUpper());

            return isExist;
        }

        private async Task<User> CreateUserAsync(UserServiceModel model, string password)
        {
            var dbUser = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                LastName = model.LastName,
                FirstName = model.FirstName
            };
            var result = await this.userManager.CreateAsync(dbUser, password);
            DataValidator.ValidateIdentityResult(result);

            return dbUser;
        }
    }
}
