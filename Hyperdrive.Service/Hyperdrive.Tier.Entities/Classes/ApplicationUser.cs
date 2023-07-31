using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.Entities.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationUser"/> class. Implements <see cref="IdentityUser{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUser : IdentityUser<int>, IKey, IBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ApplicationUser"/>
        /// </summary>
        public ApplicationUser()
        {
        }

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
        /// Gets or Sets <see cref="FirstName"/>
        /// </summary>
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LastName"/>
        /// </summary>
        [ProtectedPersonalData]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserRoles"/>
        /// </summary>
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new HashSet<ApplicationUserRole>();

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserClaims"/>
        /// </summary>
        public virtual ICollection<ApplicationUserClaim> ApplicationUserClaims { get; set; } = new HashSet<ApplicationUserClaim>();

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserLogins"/>
        /// </summary>
        public virtual ICollection<ApplicationUserLogin> ApplicationUserLogins { get; set; } = new HashSet<ApplicationUserLogin>();

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserTokens"/>
        /// </summary>
        public virtual ICollection<ApplicationUserToken> ApplicationUserTokens { get; set; } = new HashSet<ApplicationUserToken>();

        /// <summary>
        /// Gets or Sets <see cref="Archives"/>
        /// </summary>
        public virtual ICollection<Archive> Archives { get; set; } = new HashSet<Archive>();

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserArchives"/>
        /// </summary>
        public virtual ICollection<ApplicationUserArchive> ApplicationUserArchives { get; set; } = new HashSet<ApplicationUserArchive>();
    }
}
