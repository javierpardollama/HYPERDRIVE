using System;
using System.Collections.Generic;
using Hyperdrive.Domain.Dtos.Interfaces;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="ApplicationRoleDto"/> class. Implements <see cref="IKeyDto"/>, <see cref="IBaseDto"/>
/// </summary>
public class ApplicationRoleDto : IKeyDto, IBaseDto
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
    /// Gets or Sets <see cref="ImageUri"/>
    /// </summary>
    public string ImageUri { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ApplicationUsers"/>
    /// </summary>
    public virtual ICollection<CatalogDto> ApplicationUsers { get; set; } = [];
}