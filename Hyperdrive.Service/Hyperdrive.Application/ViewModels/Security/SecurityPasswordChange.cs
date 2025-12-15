using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security;

/// <summary>
/// Represents a <see cref="SecurityPasswordChange"/> class. Inherits <see cref="SecurityBase"/>
/// </summary>
public class SecurityPasswordChange : SecurityBase
{
    /// <summary>
    /// Gets or Sets <see cref="CurrentPassword"/>
    /// </summary>
    [Required]
    public string CurrentPassword { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="NewPassword"/>
    /// </summary>
    [Required]
    public string NewPassword { get; set; }
}
