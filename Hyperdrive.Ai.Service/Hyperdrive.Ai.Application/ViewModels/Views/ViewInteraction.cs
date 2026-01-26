using System;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewInteraction" /> class
/// </summary>
public class ViewInteraction
{
    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Query" />
    /// </summary>
    public virtual ViewQuery Query { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Answer" />
    /// </summary>
    public ViewAnswer Answer { get; set; }
}
