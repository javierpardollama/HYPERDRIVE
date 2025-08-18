using System;
using System.Collections.Generic;
using Hyperdrive.Domain.Dtos.Interfaces;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="ApplicationUserDto"/> class. Implements <see cref="IKeyDto"/>, <see cref="IBaseDto"/>
/// </summary>
public class ApplicationUserDto : IKeyDto, IBaseDto
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime LastModified { get; set; }

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
    /// Gets or Sets <see cref="ApplicationUserToken"/>
    /// </summary>
    public virtual ApplicationUserTokenDto ApplicationUserToken { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserRefreshToken"/>
    /// </summary>
    public virtual ApplicationUserRefreshTokenDto ApplicationUserRefreshToken { get; set; }
}