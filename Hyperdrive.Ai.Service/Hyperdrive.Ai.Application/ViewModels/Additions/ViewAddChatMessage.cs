using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddChatMessage" /> class
/// </summary>
public class ViewAddChatMessage
{
    /// <summary>
    ///     Gets or Sets <see cref="ChatId" />
    /// </summary>
    [Required]
    public Guid ChatId { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    [Required]
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    [Required]
    public Guid CreatedBy { get; set; }
}
