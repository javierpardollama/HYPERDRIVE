using System;
using System.Collections.Generic;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="ApplicationUserDto"/> class.
/// </summary>
public class ApplicationUserDto
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Email"/>
    /// </summary>
    public string Email { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="FirstName"/>
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="LastName"/>
    /// </summary>
    public string LastName { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="PhoneNumber"/>
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="Initial"/>
    /// </summary>
    public string Initial => Email?[..1].ToUpper();

    /// <summary>
    /// Gets or Sets <see cref="ApplicationRoles"/>
    /// </summary>
    public virtual ICollection<CatalogDto> ApplicationRoles { get; set; } = [];
   
    /// <summary>
    /// Gets or Sets <see cref="Token"/>
    /// </summary>
    public virtual TokenDto Token { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="RefreshToken"/>
    /// </summary>
    public virtual TokenDto RefreshToken { get; set; }
}