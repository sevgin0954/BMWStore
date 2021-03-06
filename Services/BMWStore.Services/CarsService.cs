﻿using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Services.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Services.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarsService : ICarsService
    {
		private readonly ICarRepository carRepository;

		public CarsService(ICarRepository carRepository)
        {
			this.carRepository = carRepository;
		}

        public async Task<CarServiceModel> GetByIdAsync(string carId)
        {
            var carModel = await this.carRepository
                .FindAll(carId)
                .To<CarServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(carModel, new ArgumentException(ErrorConstants.IncorrectId));

            return carModel;
        }

        public IQueryable<TCar> GetFiltered<TCar>(params ICarFilterStrategy[] filterStrategies)
            where TCar : BaseCar
        {
            var filteredCars = this.carRepository.Find(c => c is TCar).OfType<TCar>();

            foreach (var strategy in filterStrategies)
            {
                filteredCars = strategy.Filter(filteredCars).OfType<TCar>();
            }

            return filteredCars;
        }

        public IQueryable<BaseCarServiceModel> GetCars(
            ICarSortStrategy<BaseCar> sortStrategy,
            params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.GetFiltered<BaseCar>(filterStrategies);
            var filteredAndSortedCars = sortStrategy.Sort(filteredCars);

            var carModels = filteredAndSortedCars.To<BaseCarServiceModel>();

            return carModels;
        }
    }
}