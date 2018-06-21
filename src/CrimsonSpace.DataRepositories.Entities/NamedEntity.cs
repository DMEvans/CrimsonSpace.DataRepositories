namespace CrimsonSpace.DataRepositories.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base entity class with a required name
    /// </summary>
    /// <typeparam name="T">The primary key type</typeparam>
    /// <seealso cref="CrimsonSpace.DataRepositories.Entities.BaseEntity{T}" />
    public abstract class NamedEntity<T> : BaseEntity<T>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }
    }
}