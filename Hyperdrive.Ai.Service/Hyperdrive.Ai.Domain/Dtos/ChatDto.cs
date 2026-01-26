using System;
using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Dtos;

/// <summary>
///     Represents a <see cref="ChatDto" /> class
/// </summary>
public class ChatDto
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Interactions" />
    /// </summary>
    public virtual ICollection<InteractionDto> Interactions { get; set; } = [];
}
