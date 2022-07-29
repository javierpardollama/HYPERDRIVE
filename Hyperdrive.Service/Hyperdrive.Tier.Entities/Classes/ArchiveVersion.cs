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

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public float Size { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public virtual Archive Archive { get; set; }
    }
}