using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserToken"/> class. Implements <see cref="IdentityUserToken{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUserToken : IdentityUserToken<int>, IKey, IBase
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
        /// Gets or Sets <see cref="IssuedAt"/>
        /// </summary>
        [Required]
        public DateTime IssuedAt { get; set; }

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
    }
}
