using System.Collections.Generic;

namespace Hyperdrive.Main.Domain.Dtos;

/// <summary>
/// Represents a <see cref="PageDto{T}"/> class.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageDto<T>
{
    /// <summary>
    /// Gets or Sets <see cref="Length"/>
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Index"/>
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Size"/>
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Items"/>
    /// </summary>
    public ICollection<T> Items { get; set; } = [];
}