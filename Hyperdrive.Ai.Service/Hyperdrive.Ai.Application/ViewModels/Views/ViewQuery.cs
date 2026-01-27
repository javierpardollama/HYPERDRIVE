using System;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewQuery" /> class
/// </summary>
public class ViewQuery
{
    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime? LastModified { get; set; }
}
