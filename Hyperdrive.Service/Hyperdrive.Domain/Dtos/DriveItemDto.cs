using System;
using System.Collections.Generic;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="DriveItemDto"/> class.
/// </summary>
public class DriveItemDto
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="By"/>
    /// </summary>
    public CatalogDto By { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Folder"/>
    /// </summary>
    public bool Folder { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="FileName"/>
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Name"/>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Extension"/>
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Parent"/>
    /// </summary>
    public CatalogDto Parent { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="SharedWith"/>
    /// </summary>
    public virtual ICollection<CatalogDto> SharedWith { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="Downloadeable"/>
    /// </summary>
    public bool Downloadeable { get; set; }
}