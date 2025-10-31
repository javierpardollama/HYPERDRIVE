using System;
using System.ComponentModel.DataAnnotations;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserLogin"/> class. Implements <see cref="IdentityUserLogin{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUserLogin : IdentityUserLogin<int>, IBase
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
    }
}
