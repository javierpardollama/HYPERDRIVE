using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Hyperdrive.Tier.Entities.Interfaces;

namespace Hyperdrive.Tier.Entities.Classes
{
    public class ApplicationUserDriveItem : IKey, IBase
    {
        public ApplicationUserDriveItem()
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

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual DriveItem DriveItem { get; set; }

    }
}
