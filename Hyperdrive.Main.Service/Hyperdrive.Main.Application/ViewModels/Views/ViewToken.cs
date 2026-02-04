using System;

namespace Hyperdrive.Main.Application.ViewModels.Views;

/// <summary>
/// Represents a <see cref="ViewToken"/> class.
/// </summary>
public class ViewToken
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