using System;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="TokenDto"/> class.
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Gets or Sets <see cref="IssuedAt"/>
    /// </summary>
    public DateTime IssuedAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ExpiresAt"/>
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="LoginProvider"/>
    /// </summary>
    public string LoginProvider { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Value"/>
    /// </summary>
    public string Value { get; set; }
}