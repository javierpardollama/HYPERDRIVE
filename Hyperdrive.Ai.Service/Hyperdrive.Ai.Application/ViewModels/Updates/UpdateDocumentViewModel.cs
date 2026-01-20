using System;

namespace Hyperdrive.Ai.Application.ViewModels.Updates;

/// <summary>
///     Represents a <see cref="AddDocumentViewModel" /> class
/// </summary>
public class UpdateDocumentViewModel
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Name" />
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Content" />
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ModifiedBy" />
    /// </summary>
    public Guid ModifiedBy { get; set; }
}
