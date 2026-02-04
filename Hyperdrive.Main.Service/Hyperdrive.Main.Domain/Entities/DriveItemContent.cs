using Hyperdrive.Main.Domain.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Main.Domain.Entities;

/// <summary>
/// Represents a <see cref="DriveItemContent"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>  
public class DriveItemContent : IKey, IBase
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
    /// Gets or Sets <see cref="Version"/>
    /// </summary>
    [Timestamp]
    [ConcurrencyCheck]
    public byte[] Version { get; set; }

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
    /// Gets or Sets <see cref="DriveItemInfo"/>
    /// </summary>       
    public virtual DriveItemInfo DriveItemInfo { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DriveItemInfoId"/>
    /// </summary>
    [Required]
    public int? DriveItemInfoId { get; set; }
}
