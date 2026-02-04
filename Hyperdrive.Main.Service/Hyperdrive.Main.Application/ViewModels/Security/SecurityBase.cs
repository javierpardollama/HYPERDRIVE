using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Application.ViewModels.Security;

/// <summary>
/// Represents a <see cref="SecurityBase"/> class. 
/// </summary>
public class SecurityBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }
}
