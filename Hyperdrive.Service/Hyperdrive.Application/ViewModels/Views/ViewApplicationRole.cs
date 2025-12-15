using Hyperdrive.Application.ViewModels.Interfaces.Views;
using System;

namespace Hyperdrive.Application.ViewModels.Views;

/// <summary>
/// Represents a <see cref="ViewApplicationRole"/> class. Implements <see cref="IViewKey"/>, <see cref="IViewBase"/>
/// </summary>
public class ViewApplicationRole : IViewKey, IViewBase
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
    /// Gets or Sets <see cref="Name"/>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ImageUri"/>
    /// </summary>
    public string ImageUri { get; set; }       
}
