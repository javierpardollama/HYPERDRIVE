using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates;

/// <summary>
/// Represents a <see cref="UpdateDriveItem"/> class.
/// </summary>
public class UpdateDriveItem
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ParentId"/>
    /// </summary>
    [Required]
    public int ParentId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="FileName"/>
    /// </summary>
    [Required]
    public string FileName { get; set; }   

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