using BMWStore.Data.Repositories.Generic.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BMWStore.Data.Repositories.Extensions
{
    public static class DbContextFindAllExtensions
    {
        private static readonly MethodInfo ContainsMethod = typeof(Enumerable).GetMethods()
            .FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(object));

        public static IQueryable<TEntity> FindAll<TEntity>(this IRepository<TEntity> repository, params object[] keyValues)
            where TEntity : class
        {
            var entityType = repository.FindEntityType(typeof(TEntity));
            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey.Properties.Count != 1)
                throw new NotSupportedException("Only a single primary key is supported");

            var pkProperty = primaryKey.Properties[0];
            var pkPropertyType = pkProperty.ClrType;

            // validate passed key values
            foreach (var keyValue in keyValues)
            {
                if (!pkPropertyType.IsAssignableFrom(keyValue.GetType()))
                    throw new ArgumentException($"Key value '{keyValue}' is not of the right type");
            }

            // retrieve member info for primary key
            var pkMemberInfo = typeof(TEntity).GetProperty(pkProperty.Name);
            if (pkMemberInfo == null)
                throw new ArgumentException("Type does not contain the primary key as an accessible property");

            // build lambda expression
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var body = Expression.Call(null, ContainsMethod,
                Expression.Constant(keyValues),
                Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo), typeof(object)));
            var predicateExpression = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            // run query
            return repository.Find(predicateExpression);
        }
    }
}