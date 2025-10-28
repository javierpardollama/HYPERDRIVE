using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUser"/> class. Implements <see cref="IdentityUser{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUser : IdentityUser<int>, IKey, IBase
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
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserClaims"/>
        /// </summary>
        public virtual ICollection<ApplicationUserClaim> ApplicationUserClaims { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserLogins"/>
        /// </summary>
        public virtual ICollection<ApplicationUserLogin> ApplicationUserLogins { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserTokens"/>
        /// </summary>
        public virtual ICollection<ApplicationUserToken> ApplicationUserTokens { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserRefreshTokens"/>
        /// </summary>
        public virtual ICollection<ApplicationUserRefreshToken> ApplicationUserRefreshTokens { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="DriveItems"/>
        /// </summary>
        public virtual ICollection<DriveItem> DriveItems { get; set; } = [];

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserDriveItems"/>
        /// </summary>
        public virtual ICollection<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; } = [];
    }
}
