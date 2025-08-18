using System;

namespace Hyperdrive.Domain.Dtos.Interfaces;

/// <summary>
/// Represents a <see cref="IBaseDto"/> interface
/// </summary>
public interface IBaseDto
{
    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    DateTime LastModified { get; set; }
}