using System;
using System.ComponentModel.DataAnnotations;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserRole"/> class. Implements <see cref="IdentityUserRole{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUserRole : IdentityUserRole<int>, IBase
    {
        /// <summary>
        /// Gets or Sets <see cref="CreatedAt"/>
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ModifiedAt"/>
        /// </summary>       
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DeletedAt"/>
        /// </summary>       
        public DateTime? DeletedAt { get; set; }

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
        /// Gets or Sets <see cref="ApplicationRole"/>
        /// </summary>
        public virtual ApplicationRole ApplicationRole { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
