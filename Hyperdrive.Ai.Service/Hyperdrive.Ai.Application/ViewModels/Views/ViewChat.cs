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
    ///     Gets or Sets <see cref="Messages" />
    /// </summary>
    public virtual ICollection<ViewMessage> Messages { get; set; } = [];
}
