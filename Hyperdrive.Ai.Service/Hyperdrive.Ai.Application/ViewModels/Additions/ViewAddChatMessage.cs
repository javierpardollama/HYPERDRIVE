using System;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddChatMessage" /> class
/// </summary>
public class ViewAddChatMessage
{
    /// <summary>
    ///     Gets or Sets <see cref="ChatId" />
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    public Guid CreatedBy { get; set; }
}
