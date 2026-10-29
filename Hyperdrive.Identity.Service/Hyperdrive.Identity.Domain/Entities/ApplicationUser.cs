using System.ComponentModel.DataAnnotations;
using Hyperdrive.Identity.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Identity.Domain.Entities;

/// <summary>
/// Represents a <see cref="ApplicationUser"/> class. Implements <see cref="IdentityUser{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>
public partial class ApplicationUser : IdentityUser<int>, IKey, IBase
{
    /// <summary>
    /// Gets or Sets <see cref="CreatedAt"/>
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ModifiedAt"/>
    /// </summary>       
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DeletedAt"/>
    /// </summary>       
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Deleted"/>
    /// </summary>
    [Required]
    public bool Deleted { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="FirstName"/>
    /// </summary>
    [ProtectedPersonalData]
    [MaxLength(120)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="LastName"/>
    /// </summary>
    [ProtectedPersonalData]
    [MaxLength(120)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Version"/>
    /// </summary>
    [Timestamp]
    [ConcurrencyCheck]
    public byte[] Version { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="UserRoles"/>
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="UserClaims"/>
    /// </summary>
    public virtual ICollection<ApplicationUserClaim> UserClaims { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="UserLogins"/>
    /// </summary>
    public virtual ICollection<ApplicationUserLogin> UserLogins { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="Tokens"/>
    /// </summary>
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="RefreshTokens"/>
    /// </summary>
    public virtual ICollection<ApplicationUserRefreshToken> RefreshTokens { get; set; } = [];
}
