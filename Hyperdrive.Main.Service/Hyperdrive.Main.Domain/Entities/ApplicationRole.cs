using Hyperdrive.Main.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Domain.Entities;

/// <summary>
/// Represents a <see cref="Builders.ApplicationRole"/> class. Implements <see cref="IdentityRole{int}"/>, <see cref="IKey"/>, <see cref="IBase"/>
/// </summary>
public partial class ApplicationRole : IdentityRole<int>, IKey, IBase
{
    /// <summary>
    /// Gets or Sets <see cref="ImageUri"/>
    /// </summary>
    [Required]
    public string ImageUri { get; set; }

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
    /// Gets or Sets <see cref="UserRoles"/>
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="RoleClaims"/>
    /// </summary>
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = [];
}
