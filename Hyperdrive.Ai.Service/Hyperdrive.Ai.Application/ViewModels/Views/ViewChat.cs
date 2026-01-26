using System;
using System.Collections.Generic;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewChat" /> class
/// </summary>
public class ViewChat
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Interactions" />
    /// </summary>
    public virtual ICollection<ViewInteraction> Interactions { get; set; } = [];
}
