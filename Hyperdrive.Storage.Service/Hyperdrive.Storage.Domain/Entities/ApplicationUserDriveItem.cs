using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hyperdrive.Storage.Domain.Entities.Interfaces;

namespace Hyperdrive.Storage.Domain.Entities;

/// <summary>
/// Represents a <see cref="ApplicationUserDriveItem"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>
public partial class ApplicationUserDriveItem : IKey, IBase
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

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
    /// Gets or Sets <see cref="ExternalUserId"/>
    /// </summary>
    [Required]
    public int ExternalUserId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DriveItemId"/>
    /// </summary>
    [Required]
    public Guid DriveItemId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DriveItem"/>
    /// </summary>
    public virtual DriveItem DriveItem { get; set; }

}
