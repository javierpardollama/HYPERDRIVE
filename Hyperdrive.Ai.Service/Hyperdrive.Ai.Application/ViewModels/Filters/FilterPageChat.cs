using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Filters;

/// <summary>
///     Represents a <see cref="FilterPageChat" /> class.
/// </summary>
public class FilterPageChat
{
    /// <summary>
    ///     Gets or Sets <see cref="UserId" />
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Index" />
    /// </summary>
    [Required]
    public int Index { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Size" />
    /// </summary>
    [Required]
    public int Size { get; set; }
}
