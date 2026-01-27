using System;
using System.Collections.Generic;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewAnswer" /> class
/// </summary>
public class ViewAnswer
{
    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Sources" />
    /// </summary>
    public ICollection<ViewSource> Sources { get; set; }
}
