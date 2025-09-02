using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="DriveItem"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    [Index(nameof(Name), nameof(Deleted))]
    public class DriveItem : IKey, IBase
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
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        [Required]
        public virtual ApplicationUser By { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="Parent"/>
        /// </summary>
        [Required]
        public virtual DriveItem Parent { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Folder"/>
        /// </summary>
        [Required]
        public bool Folder { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="System"/>
        /// </summary>
        [Required]
        public bool System { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="SharedWith"/>
        /// </summary>
        public virtual ICollection<ApplicationUserDriveItem> SharedWith { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="Activity"/>
        /// </summary>
        public virtual ICollection<DriveItemVersion> Activity { get; set; } = [];
    }
}
