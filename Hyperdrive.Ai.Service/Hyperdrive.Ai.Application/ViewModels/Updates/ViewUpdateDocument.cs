using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Updates;

/// <summary>
///     Represents a <see cref="ViewUpdateDocument" /> class
/// </summary>
public class ViewUpdateDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    [Required]
    public string Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Name" />
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Content" />
    /// </summary>
    [Required]
    public string Content { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ModifiedBy" />
    /// </summary>
    [Required]
    public Guid ModifiedBy { get; set; }
}
