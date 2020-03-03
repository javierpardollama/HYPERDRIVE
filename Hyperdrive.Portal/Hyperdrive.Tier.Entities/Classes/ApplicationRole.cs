using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Tier.Entities.Classes
{
    public partial class ApplicationRole : IdentityRole<int>, IKey, IBase
    {
        public ApplicationRole()
        {
        }

        [Required]
        public string ImageUri { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public virtual ICollection<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
    }
}
