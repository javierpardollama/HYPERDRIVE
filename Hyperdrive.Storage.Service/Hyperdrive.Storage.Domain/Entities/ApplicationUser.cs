using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hyperdrive.Storage.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Storage.Domain.Entities;

/// <summary>
/// Represents a <see cref="ApplicationUser"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>  
public class ApplicationUser: IKey, IBase
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
    /// Gets or Sets <see cref="DriveItems"/>
    /// </summary>
    [ProtectedPersonalData]
    public string FirstName { get; set; }
        
    /// <summary>
    /// Gets or Sets <see cref="LastName"/>
    /// </summary>
    [ProtectedPersonalData]
    public string LastName { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="DriveItems"/>
    /// </summary>
    public virtual ICollection<DriveItem> DriveItems { get; set; } = [];


    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserDriveItems"/>
    /// </summary>
    public virtual ICollection<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; } = [];
}