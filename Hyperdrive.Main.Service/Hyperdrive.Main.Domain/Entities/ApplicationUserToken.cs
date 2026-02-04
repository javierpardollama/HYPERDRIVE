using Hyperdrive.Main.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Domain.Entities;

/// <summary>
/// Represents a <see cref="ApplicationUserToken"/> class. Implements <see cref="IdentityUserToken{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>
public partial class ApplicationUserToken : IdentityUserToken<int>, IBase
{
    /// <summary>
    /// Gets or Sets <see cref="ExpiresAt"/>
    /// </summary>
    [Required]
    public DateTime ExpiresAt { get; set; }

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
    /// Gets or Sets <see cref="Version"/>
    /// </summary>
    [Timestamp]
    [ConcurrencyCheck]
    public byte[] Version { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="User"/>
    /// </summary>
    public virtual ApplicationUser User { get; set; }
}
