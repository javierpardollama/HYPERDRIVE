using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Tier.Entities.Classes
{
    /// <summary>
    /// Represents a <see cref="Archive"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    [Index(nameof(Name), nameof(Deleted))]
    public class Archive : IKey, IBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="Archive"/>
        /// </summary>
        public Archive()
        {
        }

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
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

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
        /// Gets or Sets <see cref="Locked"/>
        /// </summary>
        [Required]
        public bool Locked { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserArchives"/>
        /// </summary>
        public virtual ICollection<ApplicationUserArchive> ApplicationUserArchives { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ArchiveVersions"/>
        /// </summary>
        public virtual ICollection<ArchiveVersion> ArchiveVersions { get; set; } = [];
    }
}
