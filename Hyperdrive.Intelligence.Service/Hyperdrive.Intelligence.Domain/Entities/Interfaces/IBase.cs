using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Intelligence.Domain.Entities.Interfaces;

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
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ModifiedAt"/>
    /// </summary>       
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ModifiedBy"/>
    /// </summary>      
    public int? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DeletedAt"/>
    /// </summary>       
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DeletedBy"/>
    /// </summary>      
    public int? DeletedBy { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Deleted"/>
    /// </summary>
    [Required]
    public bool Deleted { get; set; }
}