using Hyperdrive.Tier.Entities.Interfaces;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Tier.Entities.Classes
{
    public class ArchiveVersion : IKey, IBase
    {
        public ArchiveVersion()
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

        public byte[] Data { get; set; }

        public float? Size { get; set; }

        public string Type { get; set; }

        [Required]
        public virtual Archive Archive { get; set; }
    }
}