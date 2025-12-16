using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates;

/// <summary>
/// Represents a <see cref="UpdateApplicationRole"/> class. Inherits <see cref="UpdateBase"/>
/// </summary>
public class UpdateApplicationRole : UpdateBase
{
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
