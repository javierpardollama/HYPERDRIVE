using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Updates;

/// <summary>
///     Represents a <see cref="ViewUpdateChatTitle" /> class
/// </summary>
public class ViewUpdateChatTitle
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Title" />
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ModifiedBy" />
    /// </summary>
    [Required]
    public Guid ModifiedBy { get; set; }
}
