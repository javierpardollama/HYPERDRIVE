using System;
using Hyperdrive.Domain.Dtos.Interfaces;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="ApplicationUserTokenDto"/> class. Implements <see cref="IKeyDto"/>, <see cref="IBaseDto"/>
/// </summary>
public class ApplicationUserTokenDto : IKeyDto, IBaseDto
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Name"/>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="LoginProvider"/>
    /// </summary>
    public string LoginProvider { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Value"/>
    /// </summary>
    public string Value { get; set; }
}