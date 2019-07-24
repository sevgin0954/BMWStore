﻿using System.Linq;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    // TODO: Rename to FilterBaseCar
    public class FilterCarsByModelTypeStrategy : ICarFilterStrategy
    {
        private readonly string[] modelTypes;

        public FilterCarsByModelTypeStrategy(params string[] modelTypes)
        {
            DataValidator.ValidateNotEmptyCollection(modelTypes, ErrorConstants.EmptyCollection);
            this.modelTypes = modelTypes;
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars
                .Include(c => c.ModelType)
                .Where(c => this.modelTypes.Any(mt => mt == c.ModelTypeId));

            return filteredCars;
        }
    }
}
