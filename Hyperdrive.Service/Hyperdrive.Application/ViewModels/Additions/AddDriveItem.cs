using System.ComponentModel.DataAnnotations;
using Hyperdrive.Application.ViewModels.Interfaces.Additions;

namespace Hyperdrive.Application.ViewModels.Additions;

/// <summary>
/// Represents a <see cref="AddDriveItem"/> class.
/// </summary>
public class AddDriveItem : IAddBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="ParentId"/>
    /// </summary>        
    public int? ParentId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="FileName"/>
    /// </summary>
    [Required]
    public string FileName { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Folder"/>
    /// </summary>
    [Required]
    public bool Folder { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Data"/>
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Size"/>
    /// </summary>
    public float? Size { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Type"/>
    /// </summary>
    public string Type { get; set; }
}
