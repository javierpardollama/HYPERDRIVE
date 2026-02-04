using Hyperdrive.Main.Application.ViewModels.Interfaces.Additions;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Application.ViewModels.Additions;

/// <summary>
/// Represents a <see cref="AddApplicationRole"/> class. Implements <see cref="IAddBase"/>
/// </summary>
public class AddApplicationRole : IAddBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Name"/>
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ImageUri"/>
    /// </summary>
    [Required]
    public string ImageUri { get; set; }
}
