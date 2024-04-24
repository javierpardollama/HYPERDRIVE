using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Tier.Entities.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationRole"/> class. Implements <see cref="IdentityRole{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationRole : IdentityRole<int>, IKey, IBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="Arenal"/>
        /// </summary>
        public ApplicationRole()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ImageUri"/>
        /// </summary>
        [Required]
        public string ImageUri { get; set; }

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
        /// Gets or Sets <see cref="ApplicationUserRoles"/>
        /// </summary>
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationRoleClaims"/>
        /// </summary>
        public virtual ICollection<ApplicationRoleClaim> ApplicationRoleClaims { get; set; } = [];
    }
}
