using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Application.ViewModels.Security;

/// <summary>
/// Represents a <see cref="SecurityRefreshTokenReset"/> class. Inherits <see cref="SecurityBase"/>
/// </summary>
public class SecurityRefreshTokenReset : SecurityBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public string ApplicationUserRefreshToken { get; set; }
}
