using BMWStore.Common.Constants;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
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

        public async Task SeedUserAsync(string password, string email, string roleName)
        {
            if (await this.IsUserExist(email))
            {
                return;
            }
            if (await this.IsRoleExist(roleName) == false)
            {
                throw new ArgumentException(ErrorConstants.RoleNotFound);
            }

            var dbUser = await this.CreateUserAsync(password, email);
            await this.userManager.AddToRoleAsync(dbUser, roleName.ToString());
            await this.userRepository.CompleteAsync();
        }

        private Task<bool> IsUserExist(string email)
        {
            var isExist = this.userRepository.AnyAsync(u => u.NormalizedEmail == email.ToString());

            return isExist;
        }

        private async Task<bool> IsRoleExist(string roleName)
        {
            var isExist = await this.roleRepository.AnyAsync(r => r.NormalizedName == roleName.ToUpper());

            return isExist;
        }

        private async Task<User> CreateUserAsync(string password, string email)
        {
            var dbUser = new User()
            {
                UserName = email,
                Email = email
            };
            await this.userManager.CreateAsync(dbUser, password);

            return dbUser;
        }
    }
}
