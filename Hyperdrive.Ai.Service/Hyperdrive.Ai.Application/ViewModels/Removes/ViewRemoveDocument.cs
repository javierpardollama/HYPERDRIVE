using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Removes;

/// <summary>
///     Represents a <see cref="ViewRemoveDocument" /> class
/// </summary>
public class ViewRemoveDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DeletedBy" />
    /// </summary>
    [Required]
    public Guid DeletedBy { get; set; }
}
