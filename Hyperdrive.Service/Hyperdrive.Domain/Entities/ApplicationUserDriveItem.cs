using Hyperdrive.Domain.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Domain.Entities;

/// <summary>
/// Represents a <see cref="ApplicationUserClaim"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>
public partial class ApplicationUserDriveItem : IKey, IBase
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
    /// Gets or Sets <see cref="UserId"/>
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="User"/>
    /// </summary>
    public virtual ApplicationUser User { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DriveItemId"/>
    /// </summary>
    [Required]
    public int DriveItemId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DriveItem"/>
    /// </summary>
    public virtual DriveItem DriveItem { get; set; }

}
