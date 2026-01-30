using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddDocument" /> class
/// </summary>
public class ViewAddDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    [Required]
    public Guid Id { get; set; }

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
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    [Required]
    public Guid CreatedBy { get; set; }
}
