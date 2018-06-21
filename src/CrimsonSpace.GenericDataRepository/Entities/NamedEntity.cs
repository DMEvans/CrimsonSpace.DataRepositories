namespace CrimsonSpace.GenericDataRepository.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class NamedEntity<T> : BaseEntity<T>
    {
        [Required]
        public string Name { get; set; }
    }
}