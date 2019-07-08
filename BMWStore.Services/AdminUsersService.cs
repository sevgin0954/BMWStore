using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Models.UserModels.ViewModels;
using BMWStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdminUsersService(IBMWStoreUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync(IUserSortStrategy sortStrategy)
        {
            var dbUserRoleId = await this.unitOfWork.Roles
                .GetIdByNameAsync(WebConstants.UserRoleName);
            var dbUsers = await this.unitOfWork.Users
                .GetSortedWithRoleAsync(sortStrategy, dbUserRoleId);
            var models = this.mapper.Map<IEnumerable<UserAdminViewModel>>(dbUsers);

            return models;
        }

        public async Task ChangeUserLockoutStateAsync(string userId)
        {
            var dbUser = await this.unitOfWork.Users.GetByIdAsync(userId);
            if (dbUser == null)
            {
                throw new ArgumentException(ErrorConstants.IncorrectId);
            }

            
        }
    }
}
