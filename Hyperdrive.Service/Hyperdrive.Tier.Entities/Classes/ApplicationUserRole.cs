using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.Entities.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserRole"/> class. Implements <see cref="IdentityUserRole{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    public partial class ApplicationUserRole : IdentityUserRole<int>, IBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ApplicationUserLogin"/>
        /// </summary>
        public ApplicationUserRole()
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
        /// Gets or Sets <see cref="ApplicationRole"/>
        /// </summary>
        public virtual ApplicationRole ApplicationRole { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
