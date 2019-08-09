using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SeedDbRolesService : ISeedDbRolesService
    {
        private readonly IRoleRepository roleRepository;

        public SeedDbRolesService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task SeedRolesAsync(params string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await IsRoleExistAsync(roleName) == false)
                {
                    AddNewRole(roleName);
                }
            }

            await this.roleRepository.CompleteAsync();
        }

        private async Task<bool> IsRoleExistAsync(string roleName)
        {
            var normalizedRoleName = roleName.ToUpper();
            var isExist = await roleRepository.AnyAsync(r => r.NormalizedName == normalizedRoleName);

            return isExist;
        }

        private void AddNewRole(string roleName)
        {
            var dbRole = new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            this.roleRepository.Add(dbRole);
        }
    }
}
