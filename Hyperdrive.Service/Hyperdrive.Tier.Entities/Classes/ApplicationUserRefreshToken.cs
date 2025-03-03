using Hyperdrive.Tier.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Tier.Entities.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserRefreshToken"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public class ApplicationUserRefreshToken : IKey,IBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ApplicationUserRefreshToken"/>
        /// </summary>
        public ApplicationUserRefreshToken()
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
        /// Gets or Sets <see cref="ExpiresAt"/>
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Revoked"/>
        /// </summary>
        [Required]
        public bool Revoked { get; set; } = false;

        /// <summary>
        /// Gets or Sets <see cref="RevokedAt"/>
        /// </summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LoginProvider"/>
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Value"/>
        /// </summary>
        [ProtectedPersonalData]
        public string Value { get; set; }
    }
}
