namespace CrimsonSpace.DataRepositories.Interfaces
{
    using CrimsonSpace.DataRepositories.Entities;
    using CrimsonSpace.DataRepositories.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IGenericDataRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the maximum results.
        /// </summary>
        /// <value>
        /// The maximum results.
        /// </value>
        int MaxResults { get; set; }

        /// <summary>
        /// Gets or sets the number of results to skip.
        /// </summary>
        /// <value>
        /// The number of results to skip.
        /// </value>
        int SkipResults { get; set; }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="item">The items.</param>
        /// <returns>
        /// The inserted Id value
        /// </returns>
        TKey Add(TEntity item);

        /// <summary>
        /// Check if an item exists.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///   <c>true</c> if any items match the query; otherwise, <c>false</c>.
        /// </returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// All items
        /// </returns>
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="projection">The projection.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// All items
        /// </returns>
        IList<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// All items
        /// </returns>
        IList<TEntity> GetAll(
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties);

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
        IList<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> projection,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Gets results with specified filter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        IList<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

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
        IList<TResult> GetFiltered<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Gets results with specified filter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching items
        /// </returns>
        IList<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> predicate,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties);

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
        IList<TResult> GetFiltered<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            OrderByParameters<TEntity, object> orderBy,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Gets single item.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>
        /// Matching item, or null if no item found
        /// </returns>
        TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

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
        TResult GetSingle<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Removes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        void Remove(params TEntity[] items);

        /// <summary>
        /// Updates the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        void Update(params TEntity[] items);
    }
}