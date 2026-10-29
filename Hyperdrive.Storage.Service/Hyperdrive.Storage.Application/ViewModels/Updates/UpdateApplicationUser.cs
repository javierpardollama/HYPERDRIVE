using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Storage.Application.ViewModels.Updates;

/// <summary>
/// Represents a <see cref="UpdateApplicationUser"/> class.
/// </summary>
public class UpdateApplicationUser
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    [Required]
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="FirstName"/>
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="LastName"/>
    /// </summary>
    public string LastName { get; set; }
}