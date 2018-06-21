namespace CrimsonSpace.GenericDataRepository.Repositories
{
    using CrimsonSpace.GenericDataRepository.Entities;
    using CrimsonSpace.GenericDataRepository.Extensions;
    using CrimsonSpace.GenericDataRepository.Interfaces;
    using CrimsonSpace.GenericDataRepository.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Disconnected generic data repository
    /// </summary>
    /// <typeparam name="TContext">The database context type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    /// <seealso cref="FamilyShoppingList.SharedKernel.Interfaces.IGenericDataRepository{TEntity, TKey}" />
    /// <seealso cref="SharedLibrary.Interfaces.IGenericDataRepository{T2}" />
    public class DisconnectedDataRepository<TContext, TEntity, TKey> : IGenericDataRepository<TEntity, TKey>
        where TContext : DbContext
        where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the maximum results.  This is reset to zero (no maximum) after each query executed.
        /// </summary>
        /// <value>
        /// The maximum number of results.
        /// </value>
        public int MaxResults { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of results to skip.  This is reset to zero after each query executed.
        /// </summary>
        /// <value>
        /// The number of results to skip.
        /// </value>
        public int SkipResults { get; set; } = 0;

        /// <summary>
        /// Gets or sets the type of the entity model.
        /// </summary>
        /// <value>
        /// The type of the entity model.
        /// </value>
        protected Type EntityModelType { get; set; }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>
        /// The inserted Id value
        /// </returns>
        public TKey Add(TEntity item)
        {
            using (var context = GetContextInstance())
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }

            return item.Id;
        }

        /// <summary>
        /// Check if an item exists.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///   <c>true</c> if any items match the query; otherwise, <c>false</c>.
        /// </returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = GetContextInstance())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();
                return query.Any(predicate);
            }
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>All items</returns>
        public IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            Expression<Func<TEntity, TEntity>> projection = x => x;

            return GetAll(projection, null, navigationProperties);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="orderBy">The sort expression</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>All items</returns>
        public IList<TEntity> GetAll(
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            Expression<Func<TEntity, TEntity>> projection = x => x;

            return GetAll(projection, orderBy, navigationProperties);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="projection">The projection.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// All items
        /// </returns>
        public IList<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetAll(projection, null, navigationProperties);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="projection">The projection.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// All items
        /// </returns>
        public IList<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> projection,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IList<TResult> list;

            using (var context = GetContextInstance())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }

                query = query.AsNoTracking();
                query = query.ApplyOrderByParameters(orderBy);

                if (SkipResults > 0)
                {
                    query = query.Skip(SkipResults);
                }

                if (MaxResults > 0)
                {
                    query = query.Take(MaxResults);
                }

                list = query.Select(projection).ToList();
            }

            ResetProperties();

            return list;
        }

        /// <summary>
        /// Gets items based on a predefined LINQ query.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        public IList<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            Expression<Func<TEntity, TEntity>> projection = x => x;

            return GetFiltered(predicate, projection, null, navigationProperties);
        }

        /// <summary>
        /// Gets results with specified filter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        public IList<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> predicate,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            Expression<Func<TEntity, TEntity>> projection = x => x;

            return GetFiltered(predicate, projection, orderBy, navigationProperties);
        }

        /// <summary>
        /// Gets results with specified filter.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        public IList<TResult> GetFiltered<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IList<TResult> list;

            using (var context = GetContextInstance())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include<TEntity, object>(navigationProperty);
                }

                query = query.Where(predicate).AsQueryable();
                query = query.AsNoTracking();
                query = query.ApplyOrderByParameters(orderBy);

                if (SkipResults > 0)
                {
                    query = query.Skip(SkipResults);
                }

                if (MaxResults > 0)
                {
                    query = query.Take(MaxResults);
                }

                list = query.Select(projection).ToList();
            }

            ResetProperties();

            return list;
        }

        /// <summary>
        /// Gets results with specified filter.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        public IList<TResult> GetFiltered<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetFiltered(predicate, projection, null, navigationProperties);
        }

        /// <summary>
        /// Gets the single item from a predefined query.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching item if found, else null
        /// </returns>
        public TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            Expression<Func<TEntity, TEntity>> projection = x => x;

            return GetSingle(predicate, projection, navigationProperties);
        }

        /// <summary>
        /// Gets single item.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching item, or null if no item found
        /// </returns>
        public TResult GetSingle<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TResult item;

            using (var context = GetContextInstance())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include<TEntity, object>(navigationProperty);
                }

                item = query
                        .AsNoTracking()
                        .Where(predicate)
                        .Select(projection)
                        .FirstOrDefault();
            }

            ResetProperties();

            return item;
        }

        /// <summary>
        /// Removes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Remove(params TEntity[] items)
        {
            using (var context = GetContextInstance())
            {
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Update(params TEntity[] items)
        {
            using (var context = GetContextInstance())
            {
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the context instance.
        /// </summary>
        /// <returns>Context instance</returns>
        protected DbContext GetContextInstance()
        {
            return Activator.CreateInstance<TContext>();
        }

        /// <summary>
        /// Resets the properties.
        /// </summary>
        private void ResetProperties()
        {
            MaxResults = 0;
            SkipResults = 0;
        }
    }
}