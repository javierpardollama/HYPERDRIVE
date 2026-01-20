using System;

namespace Hyperdrive.Ai.Domain.Dtos;

/// <summary>
///     Represents a <see cref="MessageDto" /> class
/// </summary>
public class MessageDto
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Text" />
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="CreatedAt" />
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
