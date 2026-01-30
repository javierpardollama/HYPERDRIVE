using System;

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
    ///     Gets or Sets <see cref="Title" />
    /// </summary>
    public string Title { get; set; }
}
