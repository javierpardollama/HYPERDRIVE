using Hyperdrive.Tier.Entities.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Tier.Entities.Classes
{
    public class Archive : IKey, IBase
    {
        public Archive()
        {
        }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public virtual ApplicationUser By { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUserArchive> ApplicationUserArchives { get; set; }

        public virtual ICollection<ArchiveVersion> ArchiveVersions { get; set; }
    }
}
