using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Removes;

/// <summary>
///     Represents a <see cref="ViewRemoveChat" /> class
/// </summary>
public class ViewRemoveChat
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
