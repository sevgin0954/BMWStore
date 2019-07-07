using AutoMapper;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Models.UserModels.ViewModels;
using BMWStore.Services.Interfaces;
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
            var dbUsers = await this.unitOfWork.Users.GetSortedByAsync(sortStrategy);
            var models = this.mapper.Map<IEnumerable<UserAdminViewModel>>(dbUsers);

            return models;
        }
    }
}
