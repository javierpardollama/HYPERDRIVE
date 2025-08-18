using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Domain.Entities.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IBase"/> interface
    /// </summary>
    public interface IBase
    {
        /// <summary>
        /// Gets or Sets <see cref="LastModified"/>
        /// </summary>
        [Required]
        DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Deleted"/>
        /// </summary>
        [Required]
        bool Deleted { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }
    }
}
