using System;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddDocument" /> class
/// </summary>
public class ViewAddDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Name" />
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Content" />
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    public Guid CreatedBy { get; set; }
}
