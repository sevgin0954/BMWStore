using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SeedDbService : ISeedDbService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public SeedDbService(IBMWStoreUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task SeedAdminAsync(string password, string email)
        {
            var dbUser = await this.CreateUser(password, email);
            await this.AddRoleToUser(dbUser, WebConstants.AdminRoleName);

            await this.unitOfWork.CompleteAsync();
        }

        private async Task<User> CreateUser(string password, string email)
        {
            var dbUser = await this.unitOfWork.Users.GetByEmailOrDefault(email);

            if (dbUser == null)
            {
                dbUser = new User()
                {
                    UserName = email,
                    Email = email
                };
                await this.AddNewUserAsync(dbUser, password);
            }

            return dbUser;
        }

        private async Task AddRoleToUser(User user, string roleName)
        {
            if (await this.userManager.IsInRoleAsync(user, WebConstants.AdminRoleName) == false)
            {
                await this.userManager.AddToRoleAsync(user, WebConstants.AdminRoleName);
            }
        }

        private async Task AddNewUserAsync(User user, string password)
        {
            await this.userManager.CreateAsync(user, password);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task SeedRolesAsync(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (IsRoleExist(role) == false)
                {
                    AddNewRole(role);
                }
            }

            await this.unitOfWork.CompleteAsync();
        }

        private bool IsRoleExist(string roleName)
        {
            return unitOfWork.Roles.GetAll().Any(r => r.NormalizedName == roleName.ToUpper());
        }

        private void AddNewRole(string roleName)
        {
            var dbRole = new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            this.unitOfWork.Roles.Add(dbRole);
        }

        public async Task SeedTestDriveStatuses(params string[] statusNames)
        {
            foreach (var statusName in statusNames)
            {
                if (await this.IsStatusExistAsync(statusName) == false)
                {
                    var dbStatus = new Status()
                    {
                        Name = statusName
                    };
                    this.unitOfWork.Statuses.Add(dbStatus);
                }
            }

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        private async Task<bool> IsStatusExistAsync(string statusName)
        {
            return await this.unitOfWork.Statuses.AnyAsync(tds => tds.Name == statusName);
        }
    }
}
