using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Auth;

/// <summary>
/// Represents a <see cref="AuthSignOut"/> class.
/// </summary>
public class AuthSignOut
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public string ApplicationUserRefreshToken { get; set; }
}
