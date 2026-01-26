using System;

namespace Hyperdrive.Ai.Domain.Dtos;

/// <summary>
///     Represents a <see cref="InteractionDto" /> class
/// </summary>
public class InteractionDto
{
    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Query" />
    /// </summary>
    public virtual QueryDto Query { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Answer" />
    /// </summary>
    public AnswerDto Answer { get; set; }
}
