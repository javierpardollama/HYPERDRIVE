using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserRefreshToken"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public class ApplicationUserRefreshToken : IBase
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
        /// Gets or Sets <see cref="User"/>
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="UserId"/>
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ExpiresAt"/>
        /// </summary>
        [Required]
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
