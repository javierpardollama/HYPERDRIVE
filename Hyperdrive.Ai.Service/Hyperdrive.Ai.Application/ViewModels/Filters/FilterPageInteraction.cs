using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Filters;

/// <summary>
///     Represents a <see cref="FilterPageInteraction" /> class.
/// </summary>
public class FilterPageInteraction
{
    /// <summary>
    ///     Gets or Sets <see cref="ChatId" />
    /// </summary>
    [Required]
    public Guid ChatId { get; set; }

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
