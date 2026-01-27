using System;

namespace Hyperdrive.Ai.Application.ViewModels.Removes;

/// <summary>
///     Represents a <see cref="ViewRemoveDocument" /> class
/// </summary>
public class ViewRemoveDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DeletedBy" />
    /// </summary>
    public Guid DeletedBy { get; set; }
}
