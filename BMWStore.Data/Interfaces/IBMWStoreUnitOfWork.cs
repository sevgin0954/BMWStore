﻿using BMWStore.Data.Repositories;
using System.Threading.Tasks;

namespace BMWStore.Data.Interfaces
{
    public interface IBMWStoreUnitOfWork
    {
        NewCarRepository NewCars { get; }
        UserRepository Users { get; }
        UsedCarRepository UsedCars { get; }
        UserRoleRepository UsersRoles { get; }
        OrderRepository Orders { get; }
        RoleRepository Roles { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}