﻿using System;
using System.Linq;
using System.Linq.Expressions;
using BMWStore.Services.FilterStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Services.FilterStrategies.UserStrategies
{
    public class FilterUsersByPredicateStrategy : IUserFilterStrategy
    {
        private readonly Expression<Func<User, bool>> predicate;

        public FilterUsersByPredicateStrategy(Expression<Func<User, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public IQueryable<User> Filter(IQueryable<User> users)
        {
            var filteredUsers = users.Where(predicate);

            return filteredUsers;
        }
    }
}
