using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Storage.Application.ViewModels.Additions;

/// <summary>
/// Represents a <see cref="AddApplicationUser"/> class.
/// </summary>
public class AddApplicationUser
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