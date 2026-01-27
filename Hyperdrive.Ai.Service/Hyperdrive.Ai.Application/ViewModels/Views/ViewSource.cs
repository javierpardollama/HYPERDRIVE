using System;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewSource" /> class
/// </summary>
public class ViewSource
{
    /// <summary>
    ///     Gets or Sets <see cref="DocumentId" />
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Preview" />
    /// </summary>
    public string Preview { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime? LastModified { get; set; }
}
