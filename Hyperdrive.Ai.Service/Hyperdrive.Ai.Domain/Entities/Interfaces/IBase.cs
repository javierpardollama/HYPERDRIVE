using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Domain.Entities.Interfaces;

/// <summary>
/// Represents a <see cref="IBase"/> interface
/// </summary>
public interface IBase
{
    /// <summary>
    /// Gets or Sets <see cref="CreatedAt"/>
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="CreatedBy"/>
    /// </summary>      
    [Required]
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ModifiedAt"/>
    /// </summary>       
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ModifiedBy"/>
    /// </summary>      
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DeletedAt"/>
    /// </summary>       
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DeletedBy"/>
    /// </summary>      
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Deleted"/>
    /// </summary>
    [Required]
    public bool Deleted { get; set; }   
}