namespace CrimsonSpace.GenericDataRepository.Entities
{
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Base entity class with a generic primary key type
    /// </summary>
    /// <typeparam name="T">The primary key type</typeparam>
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}