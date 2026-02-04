using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Application.ViewModels.Security;

/// <summary>
/// Represents a <see cref="SecurityEmailChange"/> class. Inherits <see cref="SecurityBase"/>
/// </summary>
public class SecurityEmailChange : SecurityBase
{
    /// <summary>
    /// Gets or Sets <see cref="NewEmail"/>
    /// </summary>
    [Required]
    [EmailAddress]
    public string NewEmail { get; set; }
}
