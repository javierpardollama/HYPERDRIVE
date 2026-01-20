using System;

namespace Hyperdrive.Ai.Application.ViewModels.Additions;

/// <summary>
///     Represents a <see cref="ViewAddChatMessage" /> class
/// </summary>
public class ViewAddChatMessage
{
    /// <summary>
    ///     Gets or Sets <see cref="Message" />
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedBy" />
    /// </summary>
    public Guid CreatedBy { get; set; }
}
