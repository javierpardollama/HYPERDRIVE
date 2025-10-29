using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Domain.Entities
{
    /// <summary>
    /// Represents a <see cref="DriveItemVersion"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
    /// </summary>
    [Index(nameof(Name), nameof(Deleted))]
    [Index(nameof(FileName), nameof(Deleted))]
    public class DriveItemVersion : IKey, IBase
    {
        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NormalizedName"/>
        /// </summary>
        [Required]
        public string NormalizedName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="FileName"/>
        /// </summary>
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NormalizedFileName"/>
        /// </summary>
        [Required]
        public string NormalizedFileName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Extension"/>
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NormalizedExtension"/>
        /// </summary>
        public string NormalizedExtension { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Data"/>
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Size"/>
        /// </summary>
        public float? Size { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Type"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Version"/>
        /// </summary>
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Version { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DriveItem"/>
        /// </summary>
        [Required]
        public virtual DriveItem DriveItem { get; set; }
    }
}