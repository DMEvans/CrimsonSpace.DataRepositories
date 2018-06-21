namespace CrimsonSpace.DataRepositories.Entities
{
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Base entity class with a generic primary key type
    /// </summary>
    /// <typeparam name="T">The primary key type</typeparam>
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public T Id { get; set; }
    }
}