using System;
using System.ComponentModel.DataAnnotations;

using Hyperdrive.Tier.Entities.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Tier.Entities.Classes
{
    public partial class ApplicationUserRole : IdentityUserRole<int>, IBase
    {
        public ApplicationUserRole()
        {
        }

        [Required]
        public DateTime LastModified { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
