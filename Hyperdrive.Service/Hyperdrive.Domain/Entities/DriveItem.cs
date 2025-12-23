using Hyperdrive.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hyperdrive.Domain.Entities;

/// <summary>
/// Represents a <see cref="DriveItem"/> class. Implements <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>   
public class DriveItem : IKey, IBase
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
    /// Gets or Sets <see cref="ApplicationUser"/>
    /// </summary>       
    public virtual ApplicationUser By { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ById"/>
    /// </summary>
    [Required]
    public int ById { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Parent"/>
    /// </summary>     
    public virtual DriveItem Parent { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ParentId"/>
    /// </summary>        
    public int? ParentId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Folder"/>
    /// </summary>
    [Required]
    public bool Folder { get; set; }        

    /// <summary>
    /// Gets or Sets <see cref="Version"/>
    /// </summary>
    [Timestamp]
    [ConcurrencyCheck]
    public byte[] Version { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="SharedWith"/>
    /// </summary>
    public virtual ICollection<ApplicationUserDriveItem> SharedWith { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="Activity"/>
    /// </summary>
    public virtual ICollection<DriveItemInfo> Activity { get; set; } = [];
}
