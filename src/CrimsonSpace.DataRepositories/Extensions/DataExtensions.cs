namespace CrimsonSpace.DataRepositories.Extensions
{
    using CrimsonSpace.DataRepositories.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public static class DataExtensions
    {
        /// <summary>
        /// Adds and item if it does note already exist.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="dataSet">The database set.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The added item, or null if the item to add already exists</returns>
        public static T AddIfNotExists<T>(this DbSet<T> dataSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dataSet.Any(predicate) : dataSet.Any();
            return !exists ? dataSet.Add(entity) : null;
        }

        /// <summary>
        /// Applies the order by parameters to a LINQ collection.
        /// </summary>
        /// <typeparam name="T">Type of the collection being ordered</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Enumerable collection in the requested order</returns>
        public static IQueryable<T> ApplyOrderByParameters<T>(this IQueryable<T> query, OrderByParameters<T, object> orderBy)
        {
            if (orderBy == null)
            {
                return query;
            }

            if (orderBy.Direction == Enums.OrderByDirections.Descending)
            {
                return query.OrderByDescending(orderBy.SortExpression).AsQueryable();
            }
            else
            {
                return query.OrderBy(orderBy.SortExpression).AsQueryable();
            }
        }
    }
}