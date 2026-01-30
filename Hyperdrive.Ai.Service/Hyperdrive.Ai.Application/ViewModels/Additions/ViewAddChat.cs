using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddChat" /> class
/// </summary>
public class ViewAddChat
{
    /// <summary>
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    [Required]
    public Guid CreatedBy { get; set; }
}
