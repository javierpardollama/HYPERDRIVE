using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hyperdrive.Domain.Entities.Interfaces;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserClaim"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUserDriveItem : IKey, IBase
    {
        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LastModified"/>
        /// </summary>
        [Required]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Deleted"/>
        /// </summary>
        [Required]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DriveItem"/>
        /// </summary>
        public virtual DriveItem DriveItem { get; set; }

    }
}
