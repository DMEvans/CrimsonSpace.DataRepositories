namespace CrimsonSpace.GenericDataRepository.Models
{
    using CrimsonSpace.GenericDataRepository.Enums;
    using System;

    /// <summary>
    /// Order by expression container
    /// </summary>
    /// <typeparam name="T">Type of the items to be ordered</typeparam>
    /// <typeparam name="TKey">The type of the key</typeparam>
    public class OrderByParameters<T, TKey>
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public OrderByDirections Direction { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>
        /// The sort expression.
        /// </value>
        public Func<T, TKey> SortExpression { get; set; }
    }
}