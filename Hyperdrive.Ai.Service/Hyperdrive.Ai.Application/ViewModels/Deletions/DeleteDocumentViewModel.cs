using System;

namespace Hyperdrive.Ai.Application.ViewModels.Deletions;

/// <summary>
///     Represents a <see cref="DeleteDocumentViewModel" /> class
/// </summary>
public class DeleteDocumentViewModel
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
