using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Storage.Domain.Entities.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IKey"/> interface
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        Guid Id { get; set; }
    }
}
