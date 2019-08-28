﻿using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        Task<TModel> GetByIdAsync<TModel>(string carId) where TModel : class;
        Task<CarViewModel> GetCarViewModelAsync(string carId, ClaimsPrincipal user);
        Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(IQueryable<BaseCar> cars);
        Task<IEnumerable<TModel>> GetCarsModelsAsync<TModel>(
            IQueryable<BaseCar> cars,
            int pageNumber) where TModel : class;
        Task<IEnumerable<TModel>> GetCarScheduleViewModelAsync<TModel>(
            IQueryable<BaseCar> cars,
            ClaimsPrincipal user,
            int pageNumber)
            where TModel : BaseCarScheduleTestDriveViewModel;
    }
}