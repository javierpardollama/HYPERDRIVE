using System;
using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views;

/// <summary>
/// Represents a <see cref="ViewDriveItem"/> class. Implements <see cref="IViewKey"/>, <see cref="IViewBase"/>
/// </summary>
public class ViewDriveItem : IViewKey, IViewBase
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
    public ViewCatalog By { get; set; }

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
    public ViewCatalog Parent { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="SharedWith"/>
    /// </summary>
    public virtual ICollection<ViewCatalog> SharedWith { get; set; } = [];

    /// <summary>
    /// Gets or Sets <see cref="Downloadeable"/>
    /// </summary>
    public bool Downloadeable { get; set; }
}
