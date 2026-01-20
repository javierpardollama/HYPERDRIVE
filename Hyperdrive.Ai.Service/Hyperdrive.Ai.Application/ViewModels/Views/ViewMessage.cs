using System;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewMessage" /> class
/// </summary>
public class ViewMessage
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedAt" />
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
